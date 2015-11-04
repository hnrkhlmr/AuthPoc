using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
//using log4net;
using AuthPoc.ServiceAccess.API;

namespace AuthPoc.ServiceAccess.@base
{
    public abstract class WebClientWrapperBase : IDisposable
    {
        #region Private Fields

        //private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _baseUrl;
        private string _token;

        #endregion

        public string Token { set { _token = value; } }

        #region Constructors

        protected WebClientWrapperBase(string baseUrl, string token = "")
        {
            _baseUrl = baseUrl.Trim('/');

            _token = string.IsNullOrEmpty(token) && HttpContext.Current != null ? (string)HttpContext.Current.Session["ApiAccessToken"] : "";
        }

        #endregion

        protected T Get<T>(int id)
        {
            return Get<T>(id.ToString());
        }

        protected T Get<T>(string path = "")
        {
            return ExecuteStreamingRequest<T>(path, "GET");
        }

        protected T Post<T>(int id, string data)
        {
            return Post<T>(id.ToString(), data);
        }

        protected T Post<T>(string path, string data)
        {
            return ExecuteStreamingRequest<T>(path, "POST", data);
        }

        protected T PostUrlEncoded<T>(int id, string data)
        {
            return PostUrlEncoded<T>(id.ToString(), data);
        }

        protected T PostUrlEncoded<T>(string path, string data)
        {
            return ExecuteStreamingRequest<T>(path, "POST", data, "application/x-www-form-urlencoded", false);
        }

        protected T Put<T>(int id, string data)
        {
            return Put<T>(id.ToString(), data);
        }

        protected T Put<T>(string path, string data)
        {
            return ExecuteStreamingRequest<T>(path, "PUT", data);
        }

        protected bool Delete<T>(int id)
        {
            return Delete<T>(id.ToString());
        }

        protected bool Delete<T>(string path)
        {
            try
            {
                ExecuteStreamingRequest<T>(path, "DELETE");
                return true;
            }
            catch (Exception)
            {
                var baseType = typeof(T).BaseType;

                var type = baseType != null
                    ? baseType.GetGenericArguments()[0].FullName
                    : "Okänd datatyp";

                //Log.Error(string.Format("Problem med att ta bort [{0}] via DELETE-anrop mot API på URL: '{1}'.", type, BuildUrl(path)), e);

                return false;
            }
        }

        #region Private Helpers

        private string BuildUrl(string path)
        {
            return string.Format("{0}/{1}", _baseUrl.TrimEnd('/'), string.IsNullOrEmpty(path) ? string.Empty : path.TrimStart('/'));
        }

        private T ExecuteStreamingRequest<T>(string path, string method, string data = null, string contentType = "text/json", bool useToken = true, int timeout = 30000, bool keepAlive = false)
        {
            HttpWebResponse httpResponse = null;
            var url = BuildUrl(path);

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Method = method;

                httpWebRequest.Timeout = timeout;
                httpWebRequest.KeepAlive = keepAlive;

                if (useToken)
                    httpWebRequest.Headers["Authorization"] = "Bearer " + _token;

                if (UseProxy)
                    httpWebRequest.Proxy = WebProxy;

                if (data != null)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                using (httpResponse = (HttpWebResponse) httpWebRequest.GetResponse())
                {
                    var responseStream = httpResponse.GetResponseStream();
                    if (httpResponse.StatusCode == HttpStatusCode.OK && responseStream == null)
                        throw new ApiException(HttpStatusCode.OK, new ApiResultMessage(ApiResultCode.ResponseStreamNull));

                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var result = streamReader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
            }
            catch (WebException e)
            {
                string dataPart;
                try
                {
                    if (e.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse) e.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                var error = reader.ReadToEnd();
                                var responseMessage = JsonConvert.DeserializeObject<ApiResultMessage>(error);

                                var httpStatusCode = httpResponse != null
                                    ? httpResponse.StatusCode
                                    : HttpStatusCode.InternalServerError;

                                throw new ApiException(httpStatusCode, responseMessage, e);
                            }
                        }
                    }
                }
                catch (Exception e2)
                {
                    dataPart = !string.IsNullOrEmpty(data) ? string.Format(" => with data ({0})", data) : string.Empty;
                    //Log.Error(string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart), e2);
                    throw new ApiException(httpResponse != null ? httpResponse.StatusCode : HttpStatusCode.NoContent, new ApiResultMessage { Code = ApiResultCode.ResponseStreamNull }, e2);
                }

                dataPart = !string.IsNullOrEmpty(data) ? string.Format(" => with data ({0})", data) : string.Empty;
                var message = string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart);
                throw new ApiException(httpResponse != null ? httpResponse.StatusCode : HttpStatusCode.NoContent, new ApiResultMessage { Code = ApiResultCode.ResponseStreamNull, Message = message}, e);
            }
            catch (Exception e) {
                var dataPart = !string.IsNullOrEmpty(data) ? string.Format(" => with data ({0})", data) : string.Empty;
                var message = string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart);
                //Log.Error(string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart), e);
                throw new ApiException(httpResponse != null ? httpResponse.StatusCode : HttpStatusCode.NoContent, new ApiResultMessage { Code = ApiResultCode.ResponseStreamNull, Message = message }, e);
            }
        }

        private static bool UseProxy
        {
            get { return !string.IsNullOrEmpty(ProxyUrl); }
        }

        private static WebProxy WebProxy
        {
            get
            {
                var bypassOnLocal = !string.IsNullOrEmpty(ByPassOnLocal) && bool.Parse(ByPassOnLocal);
                return new WebProxy(new Uri(ProxyUrl), bypassOnLocal);
            }
        }

        private static string ByPassOnLocal
        {
            get { return ConfigurationManager.AppSettings.Get("WebApiProxyBypassOnLocal"); }
        }

        private static string ProxyUrl
        {
            get { return ConfigurationManager.AppSettings.Get("WebApiProxy"); }
        }

        #endregion

        #region Destructors

        ~WebClientWrapperBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion
    }
}

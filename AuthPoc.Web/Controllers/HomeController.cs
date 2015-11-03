using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthPoc.ServiceAccess;
using AuthPoc.ServiceAccess.API;
using AuthPoc.Web.Models;

namespace AuthPoc.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {        
        public ActionResult Index()
        {
            try
            {
                var model = new UsersViewModel();
                
                var webClient = Factory.ValuesWebClient;
                webClient.Token = AuthPocUser.AccessToken;
                model.Values = webClient.GetValues();

                var accountWebClient = Factory.AccountWebClient;
                accountWebClient.Token = AuthPocUser.AccessToken;
                model.Users = accountWebClient.GetUsers();

                return View(model);

            }
            catch (ApiException e)
            {
                if (e.StatusCode.Equals(HttpStatusCode.Unauthorized) || e.StatusCode.Equals(HttpStatusCode.NoContent))
                    return RedirectToAction("Login", "Account");
                else
                    return Redirect("https://www.google.se/search?q=henrik+holmer");
            }            
        }

        [Authorize(Roles="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //private string GetApiBaseUrlFor(string endpoint)
        //{
        //    return GetBaseUrlFor(string.Format("/api/{0}", endpoint));
        //}

        //private string GetBaseUrlFor(string endpoint)
        //{
        //    var baseUrl = "http://localhost:12796";

        //    return string.IsNullOrEmpty(endpoint)
        //        ? baseUrl
        //        : string.Format("{0}/{1}", baseUrl, endpoint.TrimStart('/'));
        //}

        //private string BuildUrl(string path)
        //{
        //    return string.Format("{0}/{1}", GetBaseUrlFor(string.Empty).TrimEnd('/'), string.IsNullOrEmpty(path) ? string.Empty : path.TrimStart('/'));
        //}

        //private T ExecuteStreamingRequest<T>(string path, string method, string data = null, string contentType = "text/json", bool useToken = true, int timeout = 30000, bool keepAlive = false)
        //{
        //    HttpWebResponse httpResponse = null;
        //    var url = path; // BuildUrl(path);

        //    try
        //    {
        //        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        //        httpWebRequest.ContentType = contentType;
        //        httpWebRequest.Method = method;

        //        httpWebRequest.Timeout = timeout;
        //        httpWebRequest.KeepAlive = keepAlive;
        //        var token = System.Web.HttpContext.Current != null ? (string)System.Web.HttpContext.Current.Session["ApiAccessToken"] : "";

        //        if (useToken)
        //            httpWebRequest.Headers["Authorization"] = "Bearer " + token;

        //        //if (UseProxy)
        //        //    httpWebRequest.Proxy = WebProxy;

        //        if (data != null)
        //        {
        //            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //            {
        //                streamWriter.Write(data);
        //                streamWriter.Flush();
        //                streamWriter.Close();
        //            }
        //        }

        //        using (httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
        //        {
        //            var responseStream = httpResponse.GetResponseStream();
        //            if (httpResponse.StatusCode == HttpStatusCode.OK && responseStream == null)
        //                throw new ApiException(HttpStatusCode.OK, new ApiResultMessage(ApiResultCode.ResponseStreamNull));

        //            using (var streamReader = new StreamReader(responseStream))
        //            {
        //                var result = streamReader.ReadToEnd();
        //                return JsonConvert.DeserializeObject<T>(result);
        //            }
        //        }
        //    }
        //    catch (WebException e)
        //    {
        //        string dataPart;
        //        try
        //        {
        //            if (e.Response != null)
        //            {
        //                using (var errorResponse = (HttpWebResponse)e.Response)
        //                {
        //                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
        //                    {
        //                        var error = reader.ReadToEnd();
        //                        var responseMessage = JsonConvert.DeserializeObject<ApiResultMessage>(error);

        //                        var httpStatusCode = httpResponse != null
        //                            ? httpResponse.StatusCode
        //                            : HttpStatusCode.InternalServerError;

        //                        throw new ApiException(httpStatusCode, responseMessage, e);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e2)
        //        {
        //            dataPart = !string.IsNullOrEmpty(data) ? string.Format(" => with data ({0})", data) : string.Empty;
        //            //Log.Error(string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart), e2);
        //            throw new ApiException(httpResponse != null ? httpResponse.StatusCode : HttpStatusCode.NoContent, new ApiResultMessage { Code = ApiResultCode.ResponseStreamNull }, e2);
        //        }

        //        dataPart = !string.IsNullOrEmpty(data) ? string.Format(" => with data ({0})", data) : string.Empty;
        //        var message = string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart);
        //        throw new ApiException(httpResponse != null ? httpResponse.StatusCode : HttpStatusCode.NoContent, new ApiResultMessage { Code = ApiResultCode.ResponseStreamNull, Message = message }, e);
        //    }
        //    catch (Exception e)
        //    {
        //        var dataPart = !string.IsNullOrEmpty(data) ? string.Format(" => with data ({0})", data) : string.Empty;
        //        var message = string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart);
        //        //Log.Error(string.Format("[{0} ({1})] {2}{3}", method.ToUpper(), contentType, url, dataPart), e);
        //        throw new ApiException(httpResponse != null ? httpResponse.StatusCode : HttpStatusCode.NoContent, new ApiResultMessage { Code = ApiResultCode.ResponseStreamNull, Message = message }, e);                
        //    }
        //}
    }
}
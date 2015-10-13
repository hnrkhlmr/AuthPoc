using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace AuthPoc.ServiceAccess.API
{
    public class ApiActionResult : IHttpActionResult
    {
        private readonly ApiResultMessage _message;
        private readonly HttpStatusCode _statusCode;

        public ApiActionResult(HttpStatusCode statusCode, ApiResultMessage message)
        {
            _statusCode = statusCode;
            _message = message;
        }

        public ApiResultMessage Message { get { return _message; } }
        public HttpStatusCode StatusCode { get { return _statusCode; } }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_message))
            };
            return Task.FromResult(response);
        }
    }
}
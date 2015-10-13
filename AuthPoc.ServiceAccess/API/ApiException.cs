using System;
using System.Net;

namespace AuthPoc.ServiceAccess.API
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, ApiResultMessage message)
            : base(message.PrettyMessage)
        {
            StatusCode = statusCode;
            ResponseMessage = message;
        }

        public ApiException(HttpStatusCode statusCode, ApiResultMessage message, Exception innerException)
            : base(message != null ? message.PrettyMessage : null, innerException)
        {
            StatusCode = statusCode;
            ResponseMessage = message;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public ApiResultMessage ResponseMessage { get; private set; }
    }
}
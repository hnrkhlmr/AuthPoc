using System;
using Newtonsoft.Json;
using AuthPoc.ServiceAccess.API;

namespace AuthPoc.ServiceAccess.@base
{
    public class ApiResponseMessage
    {
        public string Message { get; set; }

        public bool IsDefaultMessage
        {
            get { return !IsApiErrorMessage; }
        }

        public bool IsApiErrorMessage
        {
            get
            {
                try
                {
                    return ApiResultMessage != null;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public ApiResultMessage ApiResultMessage
        {
            get
            {
                return JsonConvert.DeserializeObject<ApiResultMessage>(Message);
            }
        }
    }
}
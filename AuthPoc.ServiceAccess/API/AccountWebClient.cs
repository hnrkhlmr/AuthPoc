using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPoc.ServiceAccess.@base;
using AuthPoc.DTO.Account;
using System.Web.Http;
using Newtonsoft.Json;
using AuthPoc.Api.Models;


namespace AuthPoc.ServiceAccess.API
{
    public class AccountWebClient : WebClientWrapperBase
    {
        public AccountWebClient() : base(WebClientEndpoints.AccountApiBaseUrl) { }
        public AccountWebClient(string token) : base(WebClientEndpoints.AccountApiBaseUrl, token) { }

        public Task<IHttpActionResult> Register(RegisterBindingModelDTO entity)
        {
            var model = new RegisterBindingModel() { Email = entity.Email, Password = entity.Password, ConfirmPassword = entity.Password };
            var data = JsonConvert.SerializeObject(model);
            return Post<Task<IHttpActionResult>>("Register", data);
        }

        public void Logout()
        {
            Post<string>("api/Account/Logout", "");
        }
    }
}

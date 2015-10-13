using System.Web;
using AuthPoc.DTO.Token;
using AuthPoc.ServiceAccess.@base;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthPoc.ServiceAccess.Auth
{
    public class AuthWebClient : WebClientWrapperBase
    {
        public AuthWebClient()
            : base(WebClientEndpoints.BaseUrl)
        {
            //just for compatibility
        }

        public AuthWebClient(string baseUrl)
            : base(baseUrl)
        {
        }

        public TokenResponseDTO GetToken(string userName, string password)
        {
            var data = string.Format("username={0}&password={1}&grant_type=password", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(password));

            var result = PostUrlEncoded<TokenResponseDTO>("Token", data);

            return result;
        }
        
        //public void SignOut()
        //{
        //    Post<string>("api/Account/Logout", "");
        //}
    }
}
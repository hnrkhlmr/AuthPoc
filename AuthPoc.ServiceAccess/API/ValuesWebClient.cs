using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPoc.ServiceAccess.@base;
using AuthPoc.DTO.UserInfo;

namespace AuthPoc.ServiceAccess.API
{
    public class ValuesWebClient : WebClientWrapperBase
    {
        public ValuesWebClient() : base(WebClientEndpoints.ValuesApiBaseUrl) { }
        public ValuesWebClient(string token) : base(WebClientEndpoints.ValuesApiBaseUrl, token) { }

        public IEnumerable<ValueDTO> GetValues()
        {
            return Get<List<ValueDTO>>("Get");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthPoc.DTO.UserInfo;

namespace AuthPoc.Api.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("api/Values/Get")]
        public IEnumerable<ValueDTO> Get()
        {
            return new List<ValueDTO> {
                new ValueDTO { Value1="Hej", Value2 = "Svejs"},
                new ValueDTO { Value1 = "Hej", Value2 = "Då"}
            };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

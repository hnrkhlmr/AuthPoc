using AuthPoc.DTO.Account;
using AuthPoc.ServiceAccess.@base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthPoc.ServiceAccess.API
{
    public class RoleWebClient : WebClientWrapperBase
    {
        public RoleWebClient() : base(WebClientEndpoints.RoleApiBaseUrl) { }
        public RoleWebClient(string token) : base(WebClientEndpoints.RoleApiBaseUrl, token) { }

        public IQueryable<ApplicationRoleDTO> Roles
        {
            get { throw new NotImplementedException(); }
        }

        public Task CreateAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationRoleDTO> FindByIdAsync(int roleId)
        {
            var model = new RoleIdModelDTO
            {
                RoleId = roleId
            };
            var response = Post<ApplicationRoleDTO>("FindByIdAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }
        public Task<ApplicationRoleDTO> FindByNameAsync(string roleName)
        {
            var model = new RoleNameModelDTO
            {
                RoleName = roleName
            };
            var response = Post<ApplicationRoleDTO>("FindByNameAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task UpdateAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }
    }
}

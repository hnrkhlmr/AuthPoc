using AuthPoc.ServiceAccess;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuthPoc.Web.Models
{
    public class ApplicationRoleStore<T> : IRoleStore<ApplicationRole, int>
    {
        public Task CreateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            var factory = new WebClientsFactory();
            var response = factory.RoleWebClient.DeleteAsync(role.Id);
            return response;
        }

        public Task<ApplicationRole> FindByIdAsync(int roleId)
        {
            var factory = new WebClientsFactory();
            var response = factory.RoleWebClient.FindByIdAsync(roleId).Result;
            var applicationRole = new ApplicationRole
            {
                Id = response.Id,
                Name = response.Name
            };
            return Task.FromResult(applicationRole);
        }
        public Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            var factory = new WebClientsFactory();
            var response = factory.RoleWebClient.FindByNameAsync(roleName).Result;
            var applicationRole = new ApplicationRole
            {
                Id = response.Id,
                Name = response.Name
            };
            return Task.FromResult(applicationRole);
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}
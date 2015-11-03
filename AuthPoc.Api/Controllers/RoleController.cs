using AuthPoc.Api.Models;
using AuthPoc.DTO.Account;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthPoc.Api.Controllers
{
    public class RoleController : ApiController
    {
        private ApplicationRoleManager _roleManager;
        public RoleController()
        {

        }

        public RoleController(ApplicationRoleManager roleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            RoleManager = roleManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>("");
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        public IQueryable<ApplicationRoleDTO> Roles()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task CreateAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<ApplicationRole> FindByIdAsync(int roleId)
        {
            var result = RoleManager.FindByIdAsync(roleId);
            return result;
        }
        public System.Threading.Tasks.Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            var result = RoleManager.FindByNameAsync(roleName);
            return result;
        }

        public System.Threading.Tasks.Task UpdateAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }
    }
}

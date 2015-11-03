using AuthPoc.Api.Models;
using AuthPoc.DTO.Account;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        public Task CreateAsync(ApplicationRoleDTO role)
        {
            var appRole = new ApplicationRole
            {
                Id = role.Id,
                Name = role.Name
            };
            var identityResult = RoleManager.CreateAsync(appRole);
            return identityResult;
        }

        public Task DeleteAsync(ApplicationRoleDTO role)
        {
            var appRole = new ApplicationRole
            {
                Id = role.Id,
                Name = role.Name
            };
            var identityResult = RoleManager.DeleteAsync(appRole);
            return identityResult;
        }

        public Task<ApplicationRole> FindByIdAsync(RoleIdModelDTO model)
        {
            var result = RoleManager.FindByIdAsync(model.RoleId);
            return result;
        }

        public Task<ApplicationRole> FindByNameAsync(RoleNameModelDTO model)
        {
            var result = RoleManager.FindByNameAsync(model.RoleName);
            return result;
        }

        public Task UpdateAsync(ApplicationRoleDTO role)
        {
            throw new NotImplementedException();
        }
    }
}

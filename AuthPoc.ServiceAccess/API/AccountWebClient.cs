using AuthPoc.DTO.Account;
using AuthPoc.DTO.UserInfo;
using AuthPoc.ServiceAccess.@base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace AuthPoc.ServiceAccess.API
{
    public class AccountWebClient : WebClientWrapperBase
    {
        public AccountWebClient() : base(WebClientEndpoints.AccountApiBaseUrl) { }
        public AccountWebClient(string token) : base(WebClientEndpoints.AccountApiBaseUrl, token) { }

        public IEnumerable<ValueDTO> GetValues()
        {
            return Get<List<ValueDTO>>();
        }

        public IEnumerable<ApplicationUserDTO> GetUsers()
        {
            var response = Get<List<ApplicationUserDTO>>("GetUsers");
            return response;
        }
        public Task CreateAsync(ApplicationUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var response = Post<Task>("CreateAsync", JsonConvert.SerializeObject(user));

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(ApplicationUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var response = Post<Task>("DeleteAsync", JsonConvert.SerializeObject(user));

            return Task.FromResult<object>(null);
        }

        public Task<ApplicationUserDTO> FindByNameAsync(string userName)
        {
            var model = new FindUserByNameModelDTO
            {
                UserName = userName
            };
            var response = Post<ApplicationUserDTO>("FindByNameAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task<ApplicationUserDTO> FindByIdAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<ApplicationUserDTO>("FindByIdAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task<ApplicationUserDTO> FindByEmailAsync(string email)
        {
            var model = new FindUserByEmailModelDTO
            {
                Email = email
            };
            var response = Post<ApplicationUserDTO>("FindByEmailAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task<string> GetPasswordHashAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<string>("GetPasswordHashAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }
        
        public Task SetPasswordHashAsync(int userId, string passwordHash)
        {
            var model = new SetPasswordHashModelDTO
            {
                UserId = userId,
                PasswordHash = passwordHash
            };

            var response = Post<Task>("SetPasswordHashAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task<int> GetAccessFailedCountAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<int>("GetAccessFailedCountAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task<bool> GetTwoFactorEnabledAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<bool>("GetTwoFactorEnabledAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<DateTimeOffset>("GetLockoutEndDateAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }
        public Task<bool> GetLockoutEnabledAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<bool>("GetLockoutEnabledAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }
        
        public Task<string> GetEmailAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<string>("GetEmailAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }
        public Task UpdateAsync(ApplicationUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var response = Post<Task>("UpdateAsync", JsonConvert.SerializeObject(user));
            return Task.FromResult(response);
        }
        public Task<IHttpActionResult> Register(RegisterBindingModelDTO entity)
        {
            var model = new RegisterBindingModelDTO() { Email = entity.Email, Password = entity.Password, ConfirmPassword = entity.Password };
            var data = JsonConvert.SerializeObject(model);
            var response = Post<IHttpActionResult>("Register", data);
            return Task.FromResult(response);
        }

        public Task<IHttpActionResult> Logout()
        {
            var response = Post<IHttpActionResult>("Logout", "");
            return Task.FromResult(response);
        }

        #region RoleStore
        public Task AddToRoleAsync(int userId, string roleName)
        {
            var model = new UserRoleModelDTO
            {
                RoleName = roleName,
                UserId = userId
            };
            var response = Post<Task>("AddToRoleAsync", JsonConvert.SerializeObject(model));
            return response;
        }

        public Task<IList<string>> GetRolesAsync(int userId)
        {
            var model = new UserIdModelDTO
            {
                UserId = userId
            };
            var response = Post<IList<string>>("GetRolesAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
            
        }

        public Task<bool> IsInRoleAsync(int userId, string roleName)
        {
            var model = new UserRoleModelDTO
            {
                RoleName = roleName,
                UserId = userId
            };
            var response = Post<bool>("IsInRoleAsync", JsonConvert.SerializeObject(model));
            return Task.FromResult(response);
        }

        public Task RemoveFromRoleAsync(int userId, string roleName)
        {
            var model = new UserRoleModelDTO
            {
                RoleName = roleName,
                UserId = userId
            };
            var response = Post<Task>("RemoveFromRoleAsync", JsonConvert.SerializeObject(model));
            return response;
        }
        #endregion
    }
}

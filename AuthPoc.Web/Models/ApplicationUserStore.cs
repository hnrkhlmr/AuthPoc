using AuthPoc.DTO.Account;
using AuthPoc.ServiceAccess;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuthPoc.Web.Models
{
    public class ApplicationUserStore<T> : IUserStore<ApplicationUser, int>,
        IUserPasswordStore<ApplicationUser, int>,
        IUserEmailStore<ApplicationUser, int>,
        IUserLockoutStore<ApplicationUser, int>,
        IUserTwoFactorStore<ApplicationUser, int>,
        IUserRoleStore<ApplicationUser, int>
    {

        #region Implementations of IUserStore
        public Task CreateAsync(ApplicationUser user)
        {
            var dtoUser = new ApplicationUserDTO
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                UserName = user.UserName,
                LockoutEnabled = true                
            };

            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.CreateAsync(dtoUser);

            return Task.FromResult<Object>(null);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.DeleteAsync(user.Id);

            return Task.FromResult<Object>(null);
        }
        
        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.FindByIdAsync(userId).Result;

            if (response == null)
                return Task.FromResult<ApplicationUser>(null);
            var user = new ApplicationUser();
            user.Email = response.Email;
            user.EmailConfirmed = response.EmailConfirmed;
            user.Id = response.Id;
            user.PasswordHash = response.PasswordHash;
            user.PhoneNumber = response.PhoneNumber;
            user.PhoneNumberConfirmed = response.PhoneNumberConfirmed;
            user.UserName = response.UserName;
            user.LockoutEnabled = response.LockoutEnabled;
            user.AccessFailedCount = response.AccessFailedCount;

            return Task.FromResult<ApplicationUser>(user);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.FindByNameAsync(userName).Result;

            if (response == null)
                return Task.FromResult<ApplicationUser>(null);
            var user = new ApplicationUser();
            user.Email = response.Email;
            user.EmailConfirmed = response.EmailConfirmed;
            user.Id = response.Id;
            user.PasswordHash = response.PasswordHash;
            user.PhoneNumber = response.PhoneNumber;
            user.PhoneNumberConfirmed = response.PhoneNumberConfirmed;
            user.UserName = response.UserName;
            user.LockoutEnabled = response.LockoutEnabled;
            user.AccessFailedCount = response.AccessFailedCount;

            return Task.FromResult<ApplicationUser>(user);
        }
        public Task UpdateAsync(ApplicationUser user)
        {
            var dtoUser = new ApplicationUserDTO
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                UserName = user.UserName,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount,
                Id = user.Id
            };
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.UpdateAsync(dtoUser);
            return Task.FromResult(response);
        }
        #endregion

        #region Implementations of IUserPasswordStore
        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetPasswordHashAsync(user.Id).Result;
            return Task.FromResult(response);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.SetPasswordHashAsync(user.Id, passwordHash);
            return Task.FromResult(response);
        }
        #endregion

        #region Implementations of IUserEmailStore
        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.FindByEmailAsync(email).Result;

            if (response == null)
                return Task.FromResult<ApplicationUser>(null);
            var user = new ApplicationUser();
            user.Email = response.Email;
            user.EmailConfirmed = response.EmailConfirmed;
            user.Id = response.Id;
            user.PasswordHash = response.PasswordHash;
            user.PhoneNumber = response.PhoneNumber;
            user.PhoneNumberConfirmed = response.PhoneNumberConfirmed;
            user.UserName = response.UserName;
            user.LockoutEnabled = response.LockoutEnabled;
            user.AccessFailedCount = response.AccessFailedCount;

            return Task.FromResult<ApplicationUser>(user);
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user.Email");

            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetEmailAsync(user.Id).Result;
            return Task.FromResult(response);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user.Email");

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            return Task.FromResult(1);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            var users = Helper.DeserializeFromXML<List<ApplicationUser>>();
            var userIndex = users.IndexOf(user); 
            user.EmailConfirmed = confirmed;
            users[userIndex] = user;
            Helper.SerializeToXML<List<ApplicationUser>>(users);
            return Task.FromResult(0);
        }
        #endregion

        #region Implementations of IUserLockoutStore
        public Task<int> GetAccessFailedCountAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetAccessFailedCountAsync(user.Id).Result;
            return Task.FromResult(response);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetLockoutEnabledAsync(user.Id).Result;
            return Task.FromResult(response);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetLockoutEndDateAsync(user.Id).Result;
            return Task.FromResult(response);
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.IncrementAccessFailedCountAsync(user.Id);
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.ResetAccessFailedCountAsync(user.Id);
            return response;
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.SetLockoutEnabledAsync(user.Id);
            return response;
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.SetLockoutEndDateAsync(user.Id, lockoutEnd);
            return response;
        }
        #endregion

        #region TwoFactorEnabled
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetTwoFactorEnabledAsync(user.Id).Result;
            return Task.FromResult(response);
        }
        #endregion    
    
        #region RoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.AddToRoleAsync(user.Id, roleName);
            return response;
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.GetRolesAsync(user.Id).Result;
            return Task.FromResult(response);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.IsInRoleAsync(user.Id, roleName).Result;
            return Task.FromResult(response);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            var factory = new WebClientsFactory();
            var response = factory.AccountWebClient.RemoveFromRoleAsync(user.Id, roleName);
            return response;
        }
        #endregion

        public void Dispose()
        {

        }

    }
}
using AuthPoc.Api.Models;
using AuthPoc.Api.Providers;
using AuthPoc.Api.Results;
using AuthPoc.DTO.Account;
using AuthPoc.DTO.UserInfo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace AuthPoc.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [Authorize(Roles="Admin")]
        [Route("api/Account/GetUsers")]
        public IEnumerable<ApplicationUserDTO> GetUsers()
        {
            IQueryable<ApplicationUser> users = null;
            if (UserManager.SupportsQueryableUsers)
                users = UserManager.Users;
            var dtoUsers = new List<ApplicationUserDTO>();
            foreach (var user in users)
            {
                var dtoUser = new ApplicationUserDTO
                {
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    UserName = user.UserName
                };
                dtoUsers.Add(dtoUser);
            }
            
            return dtoUsers;
        }
        
        [Route("api/Account/Get")]
        public IEnumerable<ValueDTO> Get()
        {
            return new List<ValueDTO> {
                new ValueDTO { Value1="Hej", Value2 = "Svejs"},
                new ValueDTO { Value1 = "Hej", Value2 = "Då"}
            };
        }

        [Route("api/Account/Get/{id}")]
        public ValueDTO Get(int id)
        {
            return new ValueDTO { Value1 = "Hej", Value2 = "Svejs" };
        }

        #region UserManager Implementations
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/FindByNameAsync")]
        public Task<ApplicationUser> FindByNameAsync(FindUserByNameModel model)
        {
            var user = UserManager.FindByNameAsync(model.UserName);

            return user;
        }

        [HttpPost]
        [Route("api/Account/CreateAsync")]
        public Task CreateAsync(ApplicationUserDTO user)
        {
            var appUser = new ApplicationUser
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                UserName = user.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            var identityResult = UserManager.CreateAsync(appUser);
            return Task.FromResult<object>(null);
        }

        [HttpPost]
        [Route("api/Account/UpdateAsync")]
        public Task UpdateAsync(ApplicationUserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var appUser = new ApplicationUser
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                UserName = user.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var identityResult = UserManager.UpdateAsync(appUser);

            return identityResult;
        }

        [AllowAnonymous]        
        [HttpPost]
        [Route("api/Account/FindByEmailAsync")]
        public Task<ApplicationUser> FindByEmailAsync(FindUserByEmailModel model)
        {
            var user = UserManager.FindByEmailAsync(model.Email);

            return user;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/FindByIdAsync")]
        public Task<ApplicationUser> FindByIdAsync(UserIdModel model)
        {
            var user = UserManager.FindByIdAsync(model.UserId);
            return user;
        }

        [HttpPost]
        [Route("api/Account/DeleteAsync")]
        public Task DeleteAsync(ApplicationUser user)
        {
            if (user==null)
            {
                throw new ArgumentNullException("user");
            }
            var appUser = new ApplicationUser
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                UserName = user.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var identityResult = UserManager.DeleteAsync(user).Result;
            return Task.FromResult(identityResult);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/GetLockoutEnabledAsync")]
        public Task<bool> GetLockoutEnabledAsync(UserIdModel model)
        {
            var lockout = UserManager.GetLockoutEnabledAsync(model.UserId);
            return lockout;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/GetLockoutEndDateAsync")]
        public Task<DateTimeOffset> GetLockoutEndDateAsync(UserIdModel model)
        {
            var dateTimeOffset = UserManager.GetLockoutEndDateAsync(model.UserId);
            return dateTimeOffset;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/GetPasswordHashAsync")]
        public async Task<string> GetPasswordHashAsync(UserIdModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);
            return user.PasswordHash;
        }

        [HttpPost]
        [Route("api/Account/SetPasswordHashAsync")]
        public Task SetPasswordHashAsync(SetPasswordHashModel model)
        {
            var user = UserManager.FindByIdAsync(model.UserId).Result;
            user.PasswordHash = model.PasswordHash;
            var identityResult = UserManager.UpdateAsync(user);
            return identityResult;
        }
        [HttpPost]
        [Route("api/Account/GetEmailAsync")]
        public async Task<string> GetEmailAsync(UserIdModel model)
        {
            var email = await UserManager.GetEmailAsync(model.UserId);
            return email;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/GetAccessFailedCountAsync")]
        public Task<int> GetAccessFailedCountAsync(UserIdModel model)
        {
            var count = UserManager.GetAccessFailedCountAsync(model.UserId);
            return count;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/GetTwoFactorEnabledAsync")]
        public Task<bool> GetTwoFactorEnabledAsync(UserIdModel model)
        {
            var enabled = UserManager.GetTwoFactorEnabledAsync(model.UserId);
            return enabled;
        }
#endregion

        #region RoleStore
        [HttpPost]
        [Route("api/Account/AddToRoleAsync")]
        public Task AddToRoleAsync(UserRoleModelDTO model)
        {
            var response = UserManager.AddToRoleAsync(model.UserId, model.RoleName).Result;
            return Task.FromResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/GetRolesAsync")]
        public Task<IList<string>> GetRolesAsync(UserIdModel model)
        {
            var response = UserManager.GetRolesAsync(model.UserId).Result;
            return Task.FromResult(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Account/IsInRoleAsync")]
        public Task<bool> IsInRoleAsync(UserRoleModelDTO model)
        {
            var response = UserManager.IsInRoleAsync(model.UserId, model.RoleName).Result;
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("api/Account/RemoveFromRoleAsync")]
        public Task RemoveFromRoleAsync(UserRoleModelDTO model)
        {
            var response = UserManager.RemoveFromRoleAsync(model.UserId, model.RoleName).Result;
            return Task.FromResult(response);
        }
        #endregion

        // POST api/Account/Logout
        [Route("api/Account/Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (var linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                UserName = user.UserName,
                Logins = logins
                //,
                //ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword,
                model.NewPassword);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("api/Account/Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }
        
        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        ////private class ExternalLoginData
        ////{
        ////    public string LoginProvider { get; set; }
        ////    public string ProviderKey { get; set; }
        ////    public string UserName { get; set; }

        ////    public IList<Claim> GetClaims()
        ////    {
        ////        IList<Claim> claims = new List<Claim>();
        ////        claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

        ////        if (UserName != null)
        ////        {
        ////            claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
        ////        }

        ////        return claims;
        ////    }

        ////    public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        ////    {
        ////        if (identity == null)
        ////        {
        ////            return null;
        ////        }

        ////        Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

        ////        if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
        ////            || String.IsNullOrEmpty(providerKeyClaim.Value))
        ////        {
        ////            return null;
        ////        }

        ////        if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
        ////        {
        ////            return null;
        ////        }

        ////        return new ExternalLoginData
        ////        {
        ////            LoginProvider = providerKeyClaim.Issuer,
        ////            ProviderKey = providerKeyClaim.Value,
        ////            UserName = identity.FindFirstValue(ClaimTypes.Name)
        ////        };
        ////    }
        ////}

        
        #endregion
    }
}

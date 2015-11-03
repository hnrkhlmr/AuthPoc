using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthPoc.DTO.Account
{
    public class ApplicationUserDTO 
    {
        public ApplicationUserDTO() { }

        
        public int AccessFailedCount { get; set; }
        //public ICollection<Claim> Claims { get; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Id { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        //public ICollection<TLogin> Logins { get; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        //public ICollection<ApplicationUserRoleDTO> Roles { get; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
    }
}

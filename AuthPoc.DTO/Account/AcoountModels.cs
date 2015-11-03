using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPoc.DTO.Account
{
    public class FindUserByNameModelDTO
    {
        public string UserName { get; set; }
    }

    public class FindUserByEmailModelDTO
    {
        public string Email { get; set; }
    }

    public class UserIdModelDTO
    {
        public int UserId { get; set; }
    }

    public class SetPasswordHashModelDTO
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UserRoleModelDTO
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}

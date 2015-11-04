using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPoc.DTO.Account
{
    public class FindUserByNameModelDto
    {
        public string UserName { get; set; }
    }

    public class FindUserByEmailModelDto
    {
        public string Email { get; set; }
    }

    public class UserIdModelDto
    {
        public int UserId { get; set; }
    }

    public class SetPasswordHashModelDto
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UserRoleModelDto
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }

    public class ChangePasswordBindingModelDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

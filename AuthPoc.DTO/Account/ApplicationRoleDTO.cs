using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPoc.DTO.Account
{
    public class ApplicationRoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<ApplicationUserRole> Users { get; }
    }
}

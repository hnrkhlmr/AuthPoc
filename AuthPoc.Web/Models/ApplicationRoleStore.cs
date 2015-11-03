using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthPoc.Web.Models
{
    public class ApplicationRoleStore<T> : IQueryableRoleStore<T, int>
        where T : ApplicationRole
    {
        public IQueryable<T> Roles
        {
            get { throw new NotImplementedException(); }
        }

        public System.Threading.Tasks.Task CreateAsync(T role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(T role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<T> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }
        public System.Threading.Tasks.Task<T> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateAsync(T role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
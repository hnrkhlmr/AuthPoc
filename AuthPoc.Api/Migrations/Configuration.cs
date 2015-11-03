namespace AuthPoc.Api.Migrations
{
    using AuthPoc.Api.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthPoc.Api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuthPoc.Api.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            

            const string nameAdmin = "admin@admin.com";
            const string nameUser = "user@user.com";
            const string password = "!Welcome0";
            const string roleAdmin = "Admin";
            const string roleUser = "User";

            //Create Role Admin if it does not exist
            context.Roles.AddOrUpdate(r => r.Name,
                new ApplicationRole { Name = roleAdmin });

            context.Roles.AddOrUpdate(r => r.Name,
                new ApplicationRole { Name = roleUser });

            var pw = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.Email,
                new ApplicationUser { UserName = nameAdmin, Email = nameAdmin, PasswordHash = pw.HashPassword(password), LockoutEnabled = false, SecurityStamp = Guid.NewGuid().ToString() });

            context.Users.AddOrUpdate(u => u.Email,
                new ApplicationUser { UserName = nameUser, Email = nameUser, PasswordHash = pw.HashPassword(password), LockoutEnabled = false, SecurityStamp = Guid.NewGuid().ToString() });

            // Add user admin to Role Admin if not already added
            var role = context.Roles.First(r => r.Name.Equals(roleAdmin)); //  .Find(new[] {"Id", roleAdmin} );
            var user = context.Users.First(u => u.Email.Equals(nameAdmin));
            var userHasRole = role.Users.Any(u => u.UserId.Equals(user.Id));
            if (role != null && user != null && !userHasRole)
            {
                role.Users.Add(new ApplicationUserRole { RoleId = role.Id, UserId = user.Id });
            }

            // Add user admin to Role User if not already added
            role = context.Roles.First(r => r.Name.Equals(roleUser)); //  .Find(new[] {"Id", roleAdmin} );
            user = context.Users.First(u => u.Email.Equals(nameAdmin));
            userHasRole = role.Users.Any(u => u.UserId.Equals(user.Id));
            if (role != null && user != null && !userHasRole)
            {
                role.Users.Add(new ApplicationUserRole { RoleId = role.Id, UserId = user.Id });
            }

            role = context.Roles.First(r => r.Name.Equals(roleUser));
            user = context.Users.First(r => r.Email.Equals(nameUser));
            userHasRole = role.Users.Any(u => u.UserId.Equals(user.Id));
            if (role != null && user != null && !userHasRole)
            {
                role.Users.Add(new ApplicationUserRole { RoleId = role.Id, UserId = user.Id });
            }
            context.SaveChanges();
        }
    }
}

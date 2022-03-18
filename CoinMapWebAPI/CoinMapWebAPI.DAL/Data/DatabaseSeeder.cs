using CoinMapWebAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Data
{
    public class DatabaseSeeder
    {
        private const string InitialAdminPassword = "adminpass";

        public static void Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (context.Database.EnsureCreated())
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();

                    User admin = new User()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Email = "admin@coinmapwebapi.com",
                        NormalizedEmail = "admin@coinmapwebapi.com".ToUpper(),
                        EmailConfirmed = true,
                        UserName = "admin",
                        NormalizedUserName = "admin".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        FirstName = "Initial",
                        LastName = "Admin"
                    };

                    admin.PasswordHash = hasher.HashPassword(admin, InitialAdminPassword);

                    IdentityRole adminRole = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "Admin",
                        NormalizedName = "Admin".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    };

                    IdentityRole regularRole = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "Regular",
                        NormalizedName = "Regular".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    };

                    IdentityUserRole<string> identityAdminUserRole = new IdentityUserRole<string>()
                    { RoleId = adminRole.Id, UserId = admin.Id };

                    context.Roles.Add(adminRole);
                    context.Roles.Add(regularRole);

                    context.Users.Add(admin);

                    context.UserRoles.Add(identityAdminUserRole);

                    context.SaveChanges();
                }
            }
        }
    }
}

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

                    var trezorRetailer = new Category() { CategoryName = "Trezor Retailer" };
                    var atm = new Category() { CategoryName = "ATM" };
                    var attraction = new Category() { CategoryName = "Attraction" };
                    var cafe = new Category() { CategoryName = "Cafe" };
                    var food = new Category() { CategoryName = "Food" };
                    var grocery = new Category() { CategoryName = "Grocey" };
                    var lodging = new Category() { CategoryName = "Lodging" };
                    var nightlife = new Category() { CategoryName = "Nightlife" };
                    var shopping = new Category() { CategoryName = "Shopping" };
                    var sports = new Category() { CategoryName = "Sports" };
                    var transport = new Category() { CategoryName = "Transport" };

                    context.Categories.Add(trezorRetailer);
                    context.Categories.Add(atm);
                    context.Categories.Add(attraction);
                    context.Categories.Add(cafe);
                    context.Categories.Add(food);
                    context.Categories.Add(grocery);
                    context.Categories.Add(lodging);
                    context.Categories.Add(nightlife);
                    context.Categories.Add(shopping);
                    context.Categories.Add(transport);

                    var atm1 = new Venue()
                    {
                        Category = atm,
                        Name = "Bitcoin ATM @ Father and Sons",
                        City = "Sofia",
                        Country = "Bulgaria",
                        Address = "bul. Stefan Stambolov 34"
                    };

                    var atm2 = new Venue()
                    {
                        Category = atm,
                        Name = "One And All",
                        City = "Sofia",
                        Country = "Bulgaria",
                        Address = "Tsar Samul 11"
                    };

                    var food1 = new Venue()
                    {
                        Category = food,
                        Name = "Pizzeria OroStube",
                        City = "Varna",
                        Country = "Bulgaria",
                        Address = "Ekzarh Yosif 11"
                    };

                    var food2 = new Venue()
                    {
                        Category = food,
                        Name = "Alice Homemade",
                        City = "Varna",
                        Country = "Bulgaria",
                        Address = "Tsar Simeno 9"
                    };

                    var cafe1 = new Venue()
                    {
                        Category = cafe,
                        Name = "Nova Rosti",
                        City = "Burgas",
                        Country = "Bulgaria",
                        Address = "Yanko Komitov 12"
                    };

                    var cafe2 = new Venue()
                    {
                        Category = cafe,
                        Name = "Happy Bar and Grill",
                        City = "Burgas",
                        Country = "Bulgaria",
                        Address = "Aleko Bogoridi 32"
                    };

                    var shopping1 = new Venue()
                    {
                        Category = shopping,
                        Name = "Stack-Tech Computer Repair",
                        City = "Plovdiv",
                        Country = "Bulgaria",
                        Address = "Peyo Yavorov 33"
                    };

                    var shopping2 = new Venue()
                    {
                        Category = shopping,
                        Name = "Billa",
                        City = "Plovdiv",
                        Country = "Bulgaria",
                        Address = "Hristo Batev 18"
                    };

                    context.Venues.Add(atm1);
                    context.Venues.Add(atm2);
                    context.Venues.Add(food1);
                    context.Venues.Add(food2);
                    context.Venues.Add(cafe1);
                    context.Venues.Add(cafe2);
                    context.Venues.Add(shopping1);
                    context.Venues.Add(shopping2);



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

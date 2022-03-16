using System;
using System.Linq;
using CarSee.Constants;
using Microsoft.AspNetCore.Identity;

namespace CarSee.EntityFramework
{
    public class DataSeeder
    {
        public static void SeedUser(UserManager<IdentityUser> userManager)
        {
            var userEmail = "admin@admin.com";
            var userExists = userManager.Users.Any(u => u.Email == userEmail);
            if (!userExists)
            {

                var user = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(user, "Admin123#").Result;
                var resultAddRole = userManager.AddToRoleAsync(user, Roles.ROLE_ADMIN).Result;
            }
        }

        public static void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            IdentityRole admin = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = Constants.Roles.ROLE_ADMIN
            };
            if (!roleManager.RoleExistsAsync(admin.Name).Result)
            {
                var result = roleManager.CreateAsync(admin).Result;
            }

            IdentityRole buyer = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = Constants.Roles.ROLE_BUYER
            };
            if (!roleManager.RoleExistsAsync(buyer.Name).Result)
            {
                var result = roleManager.CreateAsync(buyer).Result;
            }
        }
    }
}
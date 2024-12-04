﻿using Microsoft.AspNetCore.Identity;
using Uniqlo.Enums;
using Uniqlo.Models;
using Uniqlo.DataAccess;
using Uniqlo.Enums;
using Uniqlo.Models;

namespace WebUniqlo.Extension
{
    public static class SeedExtension
    {
        public static void UseUserSeed(this ApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                if (!RoleManager.Roles.Any())
                {
                    foreach (Roles item in Enum.GetValues(typeof(Roles)))
                    {
                        RoleManager.CreateAsync(new IdentityRole(item.ToString())).Wait();
                    }
                }
                if (!UserManager.Users.Any(x => x.NormalizedUserName == "ADMIN"))
                {
                    User u = new User
                    {
                        FullName = "admin",
                        UserName = "admin",
                        Email = "admin@mail.ru",
                        ImageUrl = "photo.jpg"
                    };
                    UserManager.CreateAsync(u, "123").Wait();
                    UserManager.AddToRoleAsync(u, nameof(Roles.Admin)).Wait();
                }
            }
        }
    }
}

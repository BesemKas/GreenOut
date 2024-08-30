using GreenOut.Data;
using Microsoft.AspNetCore.Identity;
using GreenOut.Models;
using System.Diagnostics;
using System.Net;

namespace GreenOut.Data
{
    public class Seed
    {
       

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Account>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Account()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Name = "Peter",
                        Surname = "Pan",
                        Address = "123 Main Street",
                        PhoneNumber = "0219758397"
                    };
                    await userManager.CreateAsync(newAdminUser, "Wollie123#*?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new Account()
                    {
                        UserName = "user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Name= "Tinker",
                        Surname="bell",
                        Address = "555 lala land",
                        PhoneNumber = "0275698525"
                        
                    };
                    await userManager.CreateAsync(newAppUser, "Wollie123#*");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
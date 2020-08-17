using LMS.Core.Constant;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATORS));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.User));

            string adminUserName = "admin@demo.com";
            var adminUser = new ApplicationUser { Id = "admin1", UserName = adminUserName, FirstName = "Admin", LastName = "Demo", Email = "admin@demo.com", EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATORS);

            string demoUserName = "demo@demo.com";
            var demoUser = new ApplicationUser { Id = "demo1", UserName = demoUserName, FirstName = "Demo", LastName = "Demo", Email = "demo@demo.com", EmailConfirmed = true, LibraryCardNumber = "ABCD1234" };
            await userManager.CreateAsync(demoUser, AuthorizationConstants.DEFAULT_PASSWORD);
            demoUser = await userManager.FindByNameAsync(demoUserName);
            await userManager.AddToRoleAsync(demoUser, AuthorizationConstants.Roles.User);
        }
    }
}

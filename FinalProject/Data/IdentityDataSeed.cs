using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class IdentityDataSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Vendor"));

            var adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", ImageName = "admin.jpg", Address="Cairo, Egypt", EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, "P@ssw0rd");

            var vendorUser1 = new ApplicationUser { UserName = "vendor1@gmail.com", Email = "vendor1@gmail.com", ImageName = "vendor.png", Address = "Giza, Egypt", EmailConfirmed = true };
            await userManager.CreateAsync(vendorUser1, "P@ssw0rd");

            var vendorUser2 = new ApplicationUser { UserName = "vendor2@gmail.com", Email = "vendor2@gmail.com", ImageName = "vendor.png", Address = "Alexandria, Egypt", EmailConfirmed = true };
            await userManager.CreateAsync(vendorUser2, "P@ssw0rd");

            var defaultUser = new ApplicationUser { UserName = "user@gmail.com", Email = "user@gmail.com", ImageName = "user.png", Address = "Mansoura, Egypt", EmailConfirmed = true };
            await userManager.CreateAsync(defaultUser, "P@ssw0rd");

            adminUser = await userManager.FindByNameAsync("admin@gmail.com");
            await userManager.AddToRoleAsync(adminUser, "Admin");

            vendorUser1 = await userManager.FindByNameAsync("vendor1@gmail.com");
            await userManager.AddToRoleAsync(vendorUser1, "Vendor");

            vendorUser2 = await userManager.FindByNameAsync("vendor2@gmail.com");
            await userManager.AddToRoleAsync(vendorUser2, "Vendor");

        }
    }
}

using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Adult"))
            {
                await roleManager.CreateAsync(new IdentityRole("Adult"));
            }

            // admin
            var adminEmail = "admin@befit.pl";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                
                var result = await userManager.CreateAsync(adminUser, "senwig-Kajbi2-devtem");
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // demo
            var demoEmail = "demo@befit.pl";
            var demoUser = await userManager.FindByEmailAsync(demoEmail);
            
            if (demoUser == null)
            {
                demoUser = new AppUser
                {
                    UserName = demoEmail,
                    Email = demoEmail,
                    EmailConfirmed = true
                };
                
                await userManager.CreateAsync(demoUser, "Demo123!");
                await userManager.AddToRoleAsync(demoUser, "Adult");
            }

            await context.SaveChangesAsync();
        }
    }
}
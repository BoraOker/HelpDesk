using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Models
{
    public static class SeedData {
        private const string adminRole = "Admin";
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin_123";

        public static async void TestUser(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if(context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            var user = await userManager.FindByNameAsync(adminUser);
            var role = await roleManager.FindByNameAsync(adminRole);

            if(role == null) {
                role = new AppRole{
                    Name = adminRole
                };
                await roleManager.CreateAsync(role);
            }

            if(user == null) {
                user = new AppUser{
                    FullName = "Admin",
                    UserName = adminUser,
                    Email = "boraoker42@gmail.com"             
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, adminRole);
                }else {
                    if (!await userManager.IsInRoleAsync(user, adminRole)) {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
                }
            }
        }
    }
}
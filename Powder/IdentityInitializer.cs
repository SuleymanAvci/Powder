using Microsoft.AspNetCore.Identity;
using Powder.Entities;

namespace Powder
{
    public class IdentityInitializer
    {
        public static void CreateAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                Name = "Süleyman",
                Surname = "Avcı",
                UserName = "Suleyman"
            };
            if (userManager.FindByEmailAsync("Suleyman").Result == null)
            {
                var identityResult =userManager.CreateAsync(appUser, "1").Result;
            }
            if(roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                var identityResult = roleManager.CreateAsync(role).Result;

                var result = userManager.AddToRoleAsync(appUser, role.Name).Result;
            }
        }
    }
}

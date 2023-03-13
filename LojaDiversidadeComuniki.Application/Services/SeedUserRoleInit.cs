using LojaDiversidadeComuniki.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LojaDiversidadeComuniki.Application.Services
{
    public class SeedUserRoleInit : ISeedUserRoleInit
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInit(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if(!_roleManager.RoleExistsAsync("Customer").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                };
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Administrador").Result)
            {
                IdentityRole role = new()
                {
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                };
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if(_userManager.FindByEmailAsync("admin@lojacomuniki.com.br").Result == null)
            {
                IdentityUser user = new()
                {
                    UserName = "Admin",
                    Email = "admin@lojacomuniki.com.br",
                    NormalizedEmail = "ADMIN@LOJACOMUNIKI.COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = _userManager.CreateAsync(user, "Admin@2023@").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Administrador").Wait();
                }
            }
        }
    }
}

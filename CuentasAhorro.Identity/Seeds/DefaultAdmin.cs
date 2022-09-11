using CuentasAhorro.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CuentasAhorro.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var user = await userManager.FindByNameAsync("DefaultAdmin");

            if(user == null)
            {
                user = new Usuario
                {
                    UserName = "DefaultAdmin",
                    Nombre = "Admin",
                    ApellidoPaterno = "Default",
                    ApellidoMaterno = "Default",
                    Email = "admin@cuentasahorro.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FechaCreacion = DateTime.Now
                };

                IdentityResult userResult = await userManager.CreateAsync(user, "12345*");

                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
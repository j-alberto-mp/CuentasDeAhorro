using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CuentasAhorro.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            List<string> roles = new() { "Admin" };

            foreach (var role in roles)
            {
                bool roleExist = await _roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    _ = await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Identity.Models;
using CuentasAhorro.Services.Interface;
using CuentasAhorro.Services.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace CuentasAhorro.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AccountService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response<bool>> LogInAsync(AutenticacionViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName: model.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(userName: model.UserName, password: model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return new Response<bool>(true);
                    }
                    else
                    {
                        return new Response<bool>("Contraseña incorrecta");
                    }
                }
                else
                {
                    return new Response<bool>($"No existe el usuario {model.UserName}");
                }
            }
            catch (Exception)
            {
                return new Response<bool>("Ocurrió un error al autenticar al usuario");
            }
        }

        public async Task<Response<bool>> LogOutAsync()
        {
            await _signInManager.SignOutAsync();

            return new Response<bool>(true);
        }
    }
}
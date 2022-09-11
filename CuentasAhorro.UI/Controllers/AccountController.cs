using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Services.Implementation;
using CuentasAhorro.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await accountService.LogOutAsync();
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<JsonResult> Autenticar(AutenticacionViewModel modelo)
        {
            var resultado = await accountService.LogInAsync(model: modelo);

            return Json(resultado);
        }
    }
}

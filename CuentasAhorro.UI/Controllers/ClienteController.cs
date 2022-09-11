using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ClienteController : Controller
    {
        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Detalles(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        public IActionResult Buscar()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class CuentaController : Controller
    {
        public IActionResult Aperturar(int id = 0)
        {
            ViewBag.Id = id;

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
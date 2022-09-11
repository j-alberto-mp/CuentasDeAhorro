using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
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
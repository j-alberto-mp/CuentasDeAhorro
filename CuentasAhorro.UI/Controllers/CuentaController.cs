using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    public class CuentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

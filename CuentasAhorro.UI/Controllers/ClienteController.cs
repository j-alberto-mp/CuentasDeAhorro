using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

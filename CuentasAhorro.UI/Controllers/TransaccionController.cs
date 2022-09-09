using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    public class TransaccionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

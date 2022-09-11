using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class TransaccionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

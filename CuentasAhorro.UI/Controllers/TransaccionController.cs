using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class TransaccionController : Controller
    {
        private readonly ITransaccionService transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            this.transaccionService = transaccionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ObtenerTransacciones(int id)
        {
            var resultado = await transaccionService.GetListAsync(id);

            return Json(resultado);
        }

        public async Task<JsonResult> Registrar(TransaccionViewModel modelo)
        {
            var resultado = await transaccionService.InsertAsync(modelo);

            return Json(resultado);
        }
    }
}

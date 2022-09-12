using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class CuentaController : Controller
    {
        private readonly ICuentaService cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            this.cuentaService = cuentaService;
        }

        public IActionResult Aperturar(int id = 0)
        {
            ViewBag.Id = id;

            return View();
        }

        public IActionResult Detalles(int id)
        {
            ViewBag.Id = id;

            var test = Guid.NewGuid();

            return View();
        }

        public IActionResult Buscar()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerDetalles(int id)
        {
            var resultado = await cuentaService.GetByIdAsync(id);

            return Json(resultado);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerCuentas(int id)
        {
            var resultado = await cuentaService.GetListAsync(id);

            return Json(resultado);
        }

        [HttpGet]
        public async Task<JsonResult> BuscarCuentas(CuentaClienteViewModel modelo)
        {
            var resultado = await cuentaService.SearchAsync(modelo);

            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> Guardar(CuentaViewModel modelo)
        {
            var resultado = await cuentaService.InsertAsync(modelo);

            return Json(resultado);
        }
    }
}
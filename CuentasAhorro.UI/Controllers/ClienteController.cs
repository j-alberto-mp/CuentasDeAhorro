using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CuentasAhorro.UI.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ClienteController : Controller
    {
        private readonly IClienteService clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

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

        [HttpGet]
        public async Task<JsonResult> ObtenerDetalles(int id)
        {
            var resultado = await clienteService.GetByIdAsync(id);

            return Json(resultado);
        }

        [HttpGet]
        public async Task<JsonResult> BuscarClientes(ClienteViewModel modelo)
        {
            var resultado = await clienteService.SearchAsync(modelo);

            return Json(resultado);
        }

        [HttpPost]
        public async Task<JsonResult> Guardar(ClienteViewModel modelo)
        {
            var resultado = await clienteService.InsertAsync(modelo);

            return Json(resultado);
        }
    }
}
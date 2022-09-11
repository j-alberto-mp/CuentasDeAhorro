using CuentasAhorro.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CuentasAhorro.UI.Services
{
    public class AuthenticatedService : IAuthenticatedService
    {
        public AuthenticatedService(IHttpContextAccessor httpContextAccessor)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            UsuarioId = httpContextAccessor.HttpContext?.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        public string UsuarioId { get ; }
    }
}
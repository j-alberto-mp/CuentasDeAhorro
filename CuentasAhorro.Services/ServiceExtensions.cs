using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CuentasAhorro.Services
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
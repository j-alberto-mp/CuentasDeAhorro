using CuentasAhorro.Identity.Models;
using CuentasAhorro.Repository.Context;
using CuentasAhorro.Repository.Implementation;
using CuentasAhorro.Repository.Interface;
using CuentasAhorro.Services.Implementation;
using CuentasAhorro.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CuentasAhorro.DependencyResolution
{
    public static class DependencyInjection
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection"))
            );
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DBContext>();
            //.AddDefaultTokenProviders();
        }

        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));

            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<ICuentaService, CuentaService>();
            services.AddTransient<ITransaccionService, TransaccionService>();
        }
    }
}
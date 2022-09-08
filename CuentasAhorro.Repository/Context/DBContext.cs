using CuentasAhorro.Data.Models;
using CuentasAhorro.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CuentasAhorro.Repository.Context
{
    public class DBContext : IdentityDbContext<Usuario>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<TipoTransaccion> TipoTransacciones { get; set; }
        public virtual DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
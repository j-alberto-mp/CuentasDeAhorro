using CuentasAhorro.Data.Models;
using CuentasAhorro.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasAhorro.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();

            if (!context.TipoTransacciones.Any())
            {
                string[] values = { "Depósito", "Retiro" };

                for (int i = 0; i < values.Length; i++)
                {
                    context.TipoTransacciones.Add(new TipoTransaccion { Tipo = values[i] });
                }

                context.SaveChanges();
            }
        }
    }
}

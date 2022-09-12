using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Services.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasAhorro.Services.Interface
{
    public interface IClienteService : IBaseService<ClienteViewModel>
    {
        Task<Response<List<ClienteViewModel>>> SearchAsync(ClienteViewModel model);
    }
}
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Services.Wrappers;

namespace CuentasAhorro.Services.Interface
{
    public interface IAccountService
    {
        Task<Response<bool>> LogInAsync(AutenticacionViewModel model);

        Task<Response<bool>> LogOutAsync();
    }
}

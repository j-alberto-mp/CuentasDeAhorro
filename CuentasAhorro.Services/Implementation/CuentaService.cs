using AutoMapper;
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Data.Models;
using CuentasAhorro.Repository.Interface;
using CuentasAhorro.Services.Interface;
using CuentasAhorro.Services.Wrappers;

namespace CuentasAhorro.Services.Implementation
{
    public class CuentaService : ICuentaService
    {
        private readonly ICrudRepository<Cuenta> repository;
        private readonly IMapper mapper;
        private readonly IAuthenticatedService authenticated;

        public CuentaService(ICrudRepository<Cuenta> repository, IMapper mapper, IAuthenticatedService authenticated)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.authenticated = authenticated;
        }

        public async Task<Response<CuentaViewModel>> InsertAsync(CuentaViewModel entity)
        {
            var db = mapper.Map<Cuenta>(entity);

            db.UsuarioAltaId = authenticated.UsuarioId;
            db.FechaApertura = TimeZoneInfo.ConvertTime(DateTime.Now, Helpers.GeneralHelper.TimeZone);
            db.NumeroCuenta = Helpers.GeneralHelper.AccountNumber();

            // validar que el número de cuenta asignado no exista
            while(await repository.CountAsync(q => q.NumeroCuenta.Equals(db.NumeroCuenta)) > 0)
            {
                db.NumeroCuenta = Helpers.GeneralHelper.AccountNumber();
            }

            var result = await repository.InsertAsync(db);

            return new Response<CuentaViewModel>(mapper.Map<CuentaViewModel>(result));
        }

        public async Task<Response<CuentaViewModel>> GetByIdAsync(object id)
        {
            try
            {
                var result = await repository.GetAsync(q => q.CuentaID == (int)id);

                return new Response<CuentaViewModel>(mapper.Map<CuentaViewModel>(result));
            }
            catch (Exception)
            {
                return new Response<CuentaViewModel>("Ocurrió un error al consultar el registro");
            }
        }

        public async Task<Response<List<CuentaViewModel>>> GetListAsync(object id)
        {
            try
            {
                var result = await repository.GetListAsync(q => q.ClienteID == (int)id);

                return new Response<List<CuentaViewModel>>(mapper.Map<List<CuentaViewModel>>(result));
            }
            catch (Exception)
            {
                return new Response<List<CuentaViewModel>>("Ocurrió un error al consultar los registros");
            }
        }

        public async Task<Response<bool>> UpdateAsync(CuentaViewModel entity)
        {
            var db = await repository.GetAsync(q => q.CuentaID == entity.CuentaID);

            if(db != null)
            {
                db.Saldo = entity.Saldo;

                var result = await repository.UpdateAsync(db);

                return new Response<bool>(result);
            }
            else
            {
                return new Response<bool>("No fue posible encontrar el registro");
            }
        }

        public Task<Response<bool>> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}
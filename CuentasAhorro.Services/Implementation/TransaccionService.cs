using AutoMapper;
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Data.Models;
using CuentasAhorro.Repository.Interface;
using CuentasAhorro.Services.Interface;
using CuentasAhorro.Services.Wrappers;

namespace CuentasAhorro.Services.Implementation
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ICrudRepository<Transaccion> transaccionRepository;
        private readonly ICrudRepository<TipoTransaccion> tipoTransaccionRepository;
        private readonly ICrudRepository<Cuenta> cuentaRepository;
        private readonly IMapper mapper;
        private readonly IAuthenticatedService authenticated;

        public TransaccionService(ICrudRepository<Transaccion> transaccionRepository, ICrudRepository<TipoTransaccion> tipoTransaccionRepository, ICrudRepository<Cuenta> cuentaRepository, IMapper mapper, IAuthenticatedService authenticated)
        {
            this.transaccionRepository = transaccionRepository;
            this.tipoTransaccionRepository = tipoTransaccionRepository;
            this.cuentaRepository = cuentaRepository;
            this.mapper = mapper;
            this.authenticated = authenticated;
        }

        public async Task<Response<TransaccionViewModel>> InsertAsync(TransaccionViewModel entity)
        {
            var main = await cuentaRepository.GetAsync(q => q.CuentaID == entity.CuentaID);
            var db = mapper.Map<Transaccion>(entity);

            db.TransaccionID = Guid.NewGuid().ToString();
            db.UsuarioRealizoId = authenticated.UsuarioId;
            db.FechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, Helpers.GeneralHelper.TimeZone);

            if (entity.TipoTransaccionID == 1)
            {
                main.Saldo += entity.Monto;
            }
            else
            {
                if (entity.Monto <= main.Saldo)
                {
                    main.Saldo = main.Saldo - entity.Monto;
                }
                else
                {
                    return new Response<TransaccionViewModel>("No es posible realizar la operación ya que no cuenta con el saldo suficiente");
                }
            }

            var result = await transaccionRepository.InsertAsync(db);

            if(result != null)
            {
                bool success = await cuentaRepository.UpdateAsync(main);

                if (success)
                {
                    return new Response<TransaccionViewModel>(mapper.Map<TransaccionViewModel>(result));
                }
                else
                {
                    return new Response<TransaccionViewModel>("No fue posible actualizar el saldo de la cuenta, no se realizó la operación");
                }
            }
            else
            {
                return new Response<TransaccionViewModel>("No fue posible registrar la transacción, no se realizó la operación");
            }
        }

        public Task<Response<TransaccionViewModel>> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<TransaccionViewModel>>> GetListAsync(object id)
        {
            try
            {
                var result = await transaccionRepository.GetListAsync(q => q.CuentaID == (int)id);
                var response = mapper.Map<List<TransaccionViewModel>>(result);

                foreach (var item in response)
                {
                    item.TipoTransaccion = await tipoTransaccionRepository.GetAsync(q => q.TipoTransaccionID == item.TipoTransaccionID, q => q.Tipo);
                }

                return new Response<List<TransaccionViewModel>>(response.OrderBy(q => q.FechaOperacion).ToList());
            }
            catch (Exception)
            {
                return new Response<List<TransaccionViewModel>>("Ocurrió un error al consultar las transacciones");
            }
        }

        public Task<Response<bool>> UpdateAsync(TransaccionViewModel entity)
        {
            throw new NotImplementedException();
        }
        public Task<Response<bool>> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }
    }
}
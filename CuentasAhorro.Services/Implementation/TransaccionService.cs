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
        private readonly IMapper mapper;
        private readonly IAuthenticatedService authenticated;

        public TransaccionService(ICrudRepository<Transaccion> transaccionRepository, ICrudRepository<TipoTransaccion> tipoTransaccionRepository, IMapper mapper, IAuthenticatedService authenticated)
        {
            this.transaccionRepository = transaccionRepository;
            this.tipoTransaccionRepository = tipoTransaccionRepository;
            this.mapper = mapper;
            this.authenticated = authenticated;
        }

        public async Task<Response<TransaccionViewModel>> InsertAsync(TransaccionViewModel entity)
        {
            var db = mapper.Map<Transaccion>(entity);

            db.UsuarioRealizoId = authenticated.UsuarioId;
            db.FechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, Helpers.GeneralHelper.TimeZone);

            var result = await transaccionRepository.InsertAsync(db);

            return new Response<TransaccionViewModel>(mapper.Map<TransaccionViewModel>(result));
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

                return new Response<List<TransaccionViewModel>>(response);
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
using AutoMapper;
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Data.Models;
using CuentasAhorro.Repository.Interface;
using CuentasAhorro.Services.Interface;
using CuentasAhorro.Services.Wrappers;

namespace CuentasAhorro.Services.Implementation
{
    public class ClienteService : IClienteService
    {
        private readonly ICrudRepository<Cliente> repository;
        private readonly IMapper mapper;
        private readonly IAuthenticatedService authenticated;

        public ClienteService(ICrudRepository<Cliente> repository, IMapper mapper, IAuthenticatedService authenticated)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.authenticated = authenticated;
        }

        public async Task<Response<ClienteViewModel>> InsertAsync(ClienteViewModel entity)
        {
            var db = mapper.Map<Cliente>(entity);

            db.UsuarioAltaId = authenticated.UsuarioId;
            db.FechaRegistro = TimeZoneInfo.ConvertTime(DateTime.Now, Helpers.GeneralHelper.TimeZone);

            var result = await repository.InsertAsync(db);

            return new Response<ClienteViewModel>(mapper.Map<ClienteViewModel>(result));
        }

        public async Task<Response<ClienteViewModel>> GetByIdAsync(object id)
        {
            try
            {
                var result = await repository.GetAsync(q => q.ClienteID == (int)id);

                return new Response<ClienteViewModel>(mapper.Map<ClienteViewModel>(result));
            }
            catch (Exception)
            {
                return new Response<ClienteViewModel>("Ocurrió un error al consultar los datos del cliente");
            }
        }

        public Task<Response<List<ClienteViewModel>>> GetListAsync(object id)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<bool>> UpdateAsync(ClienteViewModel entity)
        {
            var db = await repository.GetAsync(q => q.ClienteID == entity.ClienteID);

            if (db != null)
            {
                db.Nombre = entity.Nombre.Equals(db.Nombre) ? db.Nombre : entity.Nombre;
                db.ApellidoPaterno = entity.ApellidoPaterno.Equals(db.ApellidoPaterno) ? db.ApellidoPaterno : entity.ApellidoPaterno;
                db.ApellidoMaterno = entity.ApellidoMaterno.Equals(db.ApellidoMaterno) ? db.ApellidoMaterno : entity.ApellidoMaterno;

                var result = await repository.UpdateAsync(db);

                return new Response<bool>(result);
            }
            else
            {
                return new Response<bool>("No se encontró el registro del cliente");
            }
        }

        public async Task<Response<bool>> DeleteAsync(object id)
        {
            var db = await repository.GetAsync(q => q.ClienteID == (int)id);

            if(db != null)
            {
                var result = await repository.DeleteAsync(db);

                return new Response<bool>(result);
            }
            else
            {
                return new Response<bool>("No se encontró el registro del cliente");
            }
        }

        public async Task<Response<List<ClienteViewModel>>> SearchAsync(ClienteViewModel model)
        {
            var result = new List<Cliente>();

            if(model.ClienteID > 0)
            {
                result = await repository.GetListAsync(q => q.ClienteID == model.ClienteID);
            }
            else
            {
                if(!string.IsNullOrEmpty(model.Nombre) && !string.IsNullOrEmpty(model.ApellidoPaterno) && !string.IsNullOrEmpty(model.ApellidoMaterno))
                {
                    result = await repository.GetListAsync(q => q.Nombre == model.Nombre && q.ApellidoPaterno == model.ApellidoPaterno && q.ApellidoMaterno == model.ApellidoMaterno);
                }
                else
                {
                    result = await repository.GetListAsync(q => q.Nombre == model.Nombre || q.ApellidoPaterno == model.ApellidoPaterno || q.ApellidoMaterno == model.ApellidoMaterno);
                }
            }

            return new Response<List<ClienteViewModel>>(mapper.Map<List<ClienteViewModel>>(result));
        }
    }
}
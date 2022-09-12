using AutoMapper;
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Data.Models;
using CuentasAhorro.Repository.Interface;
using CuentasAhorro.Services.Interface;
using CuentasAhorro.Services.Wrappers;
using System.Collections.Generic;

namespace CuentasAhorro.Services.Implementation
{
    public class CuentaService : ICuentaService
    {
        private readonly ICrudRepository<Cuenta> repository;
        private readonly ICrudRepository<Transaccion> transaccionRepository;
        private readonly ICrudRepository<Cliente> clienteRepository;
        private readonly IMapper mapper;
        private readonly IAuthenticatedService authenticated;

        public CuentaService(ICrudRepository<Cuenta> repository, ICrudRepository<Transaccion> transaccionRepository, ICrudRepository<Cliente> clienteRepository, IMapper mapper, IAuthenticatedService authenticated)
        {
            this.repository = repository;
            this.transaccionRepository = transaccionRepository;
            this.clienteRepository = clienteRepository;
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

            if(result != null)
            {
                if (result.Saldo > 0)
                {
                    var transaction = new Transaccion
                    {
                        CuentaID = result.CuentaID,
                        FechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, Helpers.GeneralHelper.TimeZone),
                        TipoTransaccionID = 1,
                        UsuarioRealizoId = authenticated.UsuarioId,
                        Monto = result.Saldo
                    };

                    _ = await transaccionRepository.InsertAsync(transaction);
                }
            }

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

        public async Task<Response<List<CuentaClienteViewModel>>> SearchAsync(CuentaClienteViewModel model)
        {
            List<CuentaClienteViewModel> result = new();

            if (!string.IsNullOrEmpty(model.NumeroCuenta))
            {
                var cuenta = await repository.GetAsync(q => q.NumeroCuenta == model.NumeroCuenta.Replace(" ", ""));

                if (cuenta != null)
                {
                    var cliente = await clienteRepository.GetAsync(q => q.ClienteID == cuenta.ClienteID);

                    result.Add(new CuentaClienteViewModel
                    {
                        CuentaID = cuenta.CuentaID,
                        ClienteID = cliente.ClienteID,
                        NumeroCuenta = cuenta.NumeroCuenta,
                        Nombre = cliente.Nombre,
                        ApellidoPaterno = cliente.ApellidoPaterno,
                        ApellidoMaterno = cliente.ApellidoMaterno
                    });
                }
            }
            else
            {
                List<Cliente> clientes = new();

                if(model.ClienteID > 0)
                {
                    clientes = await clienteRepository.GetListAsync(q => q.ClienteID == model.ClienteID);
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.Nombre) && !string.IsNullOrEmpty(model.ApellidoPaterno) && !string.IsNullOrEmpty(model.ApellidoMaterno))
                    {
                        clientes = await clienteRepository.GetListAsync(q => q.Nombre == model.Nombre && q.ApellidoPaterno == model.ApellidoPaterno && q.ApellidoMaterno == model.ApellidoMaterno);
                    }
                    else
                    {
                        clientes = await clienteRepository.GetListAsync(q => q.Nombre == model.Nombre || q.ApellidoPaterno == model.ApellidoPaterno || q.ApellidoMaterno == model.ApellidoMaterno);
                    }
                }

                foreach (var cliente in clientes)
                {
                    var cuentas = await repository.GetListAsync(q => q.ClienteID == cliente.ClienteID);

                    foreach (var cuenta in cuentas)
                    {
                        result.Add(new CuentaClienteViewModel
                        {
                            CuentaID = cuenta.CuentaID,
                            ClienteID = cliente.ClienteID,
                            NumeroCuenta = cuenta.NumeroCuenta,
                            Nombre = cliente.Nombre,
                            ApellidoPaterno = cliente.ApellidoPaterno,
                            ApellidoMaterno = cliente.ApellidoMaterno
                        });
                    }
                }
            }

            return new Response<List<CuentaClienteViewModel>>(result);
        }
    }
}
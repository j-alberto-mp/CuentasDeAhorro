using AutoMapper;
using CuentasAhorro.Application.ViewModels;
using CuentasAhorro.Data.Models;

namespace CuentasAhorro.Services.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Cuenta, CuentaViewModel>().ReverseMap();
            CreateMap<Transaccion, TransaccionViewModel>().ReverseMap();
        }
    }
}
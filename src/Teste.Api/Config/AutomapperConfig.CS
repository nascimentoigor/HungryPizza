using AutoMapper;
using Teste.Application.ViewModel;
using Teste.Domain.Entities;

namespace Teste.Api.Config
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Pedido, PedidoVM>().ReverseMap();
        }
    }
}

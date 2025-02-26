using AutoMapper;
using WebProov_API.Dtos;
using WebProov_API.Models;

namespace WebProov_API.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {

            CreateMap<Documento, PedidoRead>();
            CreateMap<Documento, PedidoByRucRead>();
        }
    }
}

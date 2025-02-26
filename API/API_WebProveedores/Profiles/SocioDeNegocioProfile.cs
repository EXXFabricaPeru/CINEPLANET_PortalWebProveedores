using AutoMapper;
using WebProov_API.Dtos;
using WebProov_API.Models;

namespace WebProov_API.Profiles
{
    public class SocioDeNegocioProfile : Profile
    {
        public SocioDeNegocioProfile()
        {
            CreateMap<SocioDeNegocioCreate, SocioDeNegocio>();
            CreateMap<SocioDeNegocio, SocioRead>();
        }
    }
}

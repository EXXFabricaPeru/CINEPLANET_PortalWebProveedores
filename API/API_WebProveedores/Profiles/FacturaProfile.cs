using AutoMapper;
using WebProov_API.Dtos;
using WebProov_API.Models;

namespace WebProov_API.Profiles
{
    public class FacturaProfile : Profile
    {
        public FacturaProfile()
        {

            CreateMap<Documento, FacturaRead>();
            CreateMap<Documento, FacturaByFPRucRead>();
        }
    }
}

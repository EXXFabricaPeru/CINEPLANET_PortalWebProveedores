using WebProov_API.Models;

namespace WebProov_API.Data.Interfaces
{
    public interface IBusinessPartnerRepository
    {
        public bool CrearSocio(SocioDeNegocio socio);
        public bool ActualizarSocio(SocioDeNegocio socio);
        public SocioDeNegocio GetSocioByRUCRaz(string id);
        public SocioDeNegocio GetSocioByCardCode(string id);
    }
}
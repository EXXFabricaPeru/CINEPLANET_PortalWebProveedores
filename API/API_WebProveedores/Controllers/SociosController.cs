using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProov_API.Data.Interfaces;
using WebProov_API.Dtos;
using WebProov_API.Models;

namespace WebProov_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        private IBusinessPartnerRepository _repo;
        private IMapper _mapper;

        public SociosController(IBusinessPartnerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetSocioByRUCRaz")]
        public ActionResult<SocioRead> GetSocioByRUCRaz(string id)
        {
            try
            {
                var socio = _repo.GetSocioByCardCode(id);

                if (socio == null)
                    return NotFound();

                return Ok(_mapper.Map<SocioRead>(socio));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(id, ex.Message));
            }
        }
#if DEBUG
        [HttpPost]
        public ActionResult<SocioDeNegocioCreate> CrearSocio(SocioDeNegocioCreate socio)
        {
            string CardCode = "";
            try
            {
                var socioModel = _mapper.Map<SocioDeNegocio>(socio);
                CardCode = socioModel.CardType + socioModel.LicTradNum.PadLeft(12, '0');
                socioModel.CardCode = CardCode;

                _repo.CrearSocio(socioModel);
                var socioRead = _mapper.Map<SocioRead>(socioModel);
                return CreatedAtRoute(nameof(GetSocioByRUCRaz), new { id = socioRead.CardCode }, socioRead);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(CardCode, ex.Message));
            }
        }

        [HttpPatch("{id}", Name = "UpdateSocio")]
        public ActionResult<SocioDeNegocioCreate> ActualizarSocio(string id, SocioDeNegocioCreate socio)
        {
            try
            {
                var socioModel = _mapper.Map<SocioDeNegocio>(socio);
                socioModel.CardCode = id;
                _repo.ActualizarSocio(socioModel);

                var socioRead = _mapper.Map<SocioRead>(socioModel);
                socioRead.CardCode = socioRead.CardType + socioRead.LicTradNum;
                return Ok("{\"msj\":\"Actualizado\"}");
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(id, ex.Message));
            }
        }
#endif

    }
}
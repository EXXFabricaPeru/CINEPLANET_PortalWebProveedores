using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebProov_API.Data.Interfaces;
using WebProov_API.Dtos;
using WebProov_API.Models;

namespace WebProov_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaRepository _repo;
        private IMapper _mapper;

        public FacturaController(IFacturaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet("FechaPago/{ruc}")]
        public ActionResult<List<FacturaByFPRucRead>> GetFacturaByFPRucList(string ruc)
        {
            try
            {
                var facturas = _repo.GetListaFechaPagoByRuc(ruc);

                if (facturas == null)
                    return NotFound();

                return Ok(_mapper.Map<List<FacturaByFPRucRead>>(facturas));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj("0", ex.Message));
            }
        }
#if DEBUG
        [HttpGet("{id}", Name = "GetFacturaById")]
        public ActionResult<FacturaRead> GetFacturaById(int id)
        {
            try
            {
                var pedido = _repo.GetDocumentoById(id);

                if (pedido == null)
                    return NotFound();

                return Ok(_mapper.Map<FacturaRead>(pedido));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(id.ToString(), ex.Message));
            }
        }

        [HttpPost]
        public ActionResult<FacturaRead> CrearFactura(FacturaRead oferta)
        {
            int entry = 0;
            try
            {
                var ofertaModel = _mapper.Map<Documento>(oferta);
                entry = _repo.CrearDocument(ofertaModel);

                var ofertaRead = _mapper.Map<FacturaRead>(ofertaModel);
                return CreatedAtRoute(nameof(GetFacturaById), new { id = entry }, ofertaRead);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(entry.ToString(), ex.Message));
            }
        }

        [HttpPatch("{id}", Name = "UpdateFactura")]
        public ActionResult<FacturaRead> ActualizarFactura(string id, FacturaRead oferta)
        {
            try
            {
                var ofertaModel = _mapper.Map<Documento>(oferta);
                ofertaModel.DocEntry = int.Parse(id);
                _repo.ActualizarDocumento(ofertaModel);

                var socioRead = _mapper.Map<FacturaRead>(ofertaModel);
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
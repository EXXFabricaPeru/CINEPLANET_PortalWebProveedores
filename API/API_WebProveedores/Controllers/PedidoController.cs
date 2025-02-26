using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class PedidoController : ControllerBase
    {
        private IPedidoRepository _repo;
        private IMapper _mapper;

        public PedidoController(IPedidoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetPedidoById")]
        public ActionResult<PedidoRead> GetPedidoById(int id)
        {
            try
            {
                var pedido = _repo.GetDocumentoById(id);

                if (pedido == null)
                    return NotFound();

                return Ok(_mapper.Map<PedidoRead>(pedido));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(id.ToString(), ex.Message));
            }
        }
        [HttpGet("RUC/{ruc}")]
        public ActionResult<List<PedidoByRucRead>> GetPedidoByRucList(string ruc)
        {
            try
            {
                var pedido = _repo.GetListaByRuc(ruc);

                if (pedido == null)
                    return NotFound();

                return Ok(_mapper.Map<List<PedidoByRucRead>>(pedido));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj("0", ex.Message));
            }
        }
#if DEBUG
        [HttpPost]
        public ActionResult<PedidoRead> CrearPedido(PedidoRead oferta)
        {
            int entry = 0;
            try
            {
                var ofertaModel = _mapper.Map<Documento>(oferta);
                entry = _repo.CrearDocument(ofertaModel);

                var ofertaRead = _mapper.Map<PedidoRead>(ofertaModel);
                return CreatedAtRoute(nameof(GetPedidoById), new { id = entry }, ofertaRead);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ErrMsj(entry.ToString(), ex.Message));
            }
        }

        [HttpPatch("{id}", Name = "UpdatePedido")]
        public ActionResult<PedidoRead> ActualizarPedido(string id, PedidoRead oferta)
        {
            try
            {
                var ofertaModel = _mapper.Map<Documento>(oferta);
                ofertaModel.DocEntry = int.Parse(id);
                _repo.ActualizarDocumento(ofertaModel);

                var socioRead = _mapper.Map<PedidoRead>(ofertaModel);
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
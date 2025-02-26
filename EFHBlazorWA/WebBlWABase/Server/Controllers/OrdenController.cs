using EFHBlazzer.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFHBlazzer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenController : Controller
    {
        private readonly IConfiguration _configuration;
        public OrdenController(
IConfiguration configuration)
        {

            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpGet("listado")]
        public async Task<ActionResult<List<Pedido_Lista>>> GetListado()
        {

            var client = new RestClient(_configuration["path:API"] + "Pedido/RUC/" + HttpContext.User.FindFirst("ruc").Value);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            List<Pedido_Lista> lista = new List<Pedido_Lista>();
            if (!(res.Contains("Error") || res.Contains("404")))
            {
                lista = SimpleJson.DeserializeObject<List<Pedido_Lista>>(res);
            }
            return lista;

        }

        [AllowAnonymous]
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Pedido>> GetOrden(string id)
        {
            var client = new RestClient(_configuration["path:API"] + "Pedido/" + id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            Pedido ped = new Pedido();
            if (!res.Contains("Error"))
            {
                ped= SimpleJson.DeserializeObject<Pedido>(res);
                ped.subTotal = ped.docTotal - ped.vatSum;
                ped.subTotalFC = ped.docTotalFC - ped.vatSumFC;
                return ped;
            }
            else
            {
                return BadRequest("No existe el proveedor.");
            }
        }
    }
}

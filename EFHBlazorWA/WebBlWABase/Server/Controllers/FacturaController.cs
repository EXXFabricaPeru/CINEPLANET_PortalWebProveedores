
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
    public class FacturaController : Controller
    {
        private readonly IConfiguration _configuration;
        public FacturaController(
IConfiguration configuration)
        {

            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpGet("listadoPorFecha")]
        public async Task<ActionResult<List<Factura_Lista>>> GetListadoPorFecha()
        {

            var client = new RestClient(_configuration["path:API"] + "Factura/FechaPago/" + HttpContext.User.FindFirst("ruc").Value);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            List<Factura_Lista> lista = new List<Factura_Lista>();
            if (!res.Contains("Error"))
            {
                lista= SimpleJson.DeserializeObject<List<Factura_Lista>>(res);
            }
            return lista;

        }
    }
}

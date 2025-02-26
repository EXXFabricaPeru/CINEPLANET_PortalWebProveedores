using EFHBlazzer.Shared.DTOs;
using EFHBlazzer.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EFHBlazzer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClienteController : Controller
    {
        private readonly IConfiguration _configuration;
        public ClienteController(
IConfiguration configuration)
        {

            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ActionResult<Cliente_Head>> Get()
        {
            var client = new RestClient(_configuration["path:API"] + "Socios/" + HttpContext.User.Identity.Name);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
            var res = response.Content;
            if (!res.Contains("Error"))
            {
                return SimpleJson.DeserializeObject<Cliente_Head>(res);
            }
            else
            {
                return BadRequest("No existe el proveedor.");
            }
        }
    }
}

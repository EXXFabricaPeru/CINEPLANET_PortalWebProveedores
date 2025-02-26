using EFHBlazzer.Shared.DTOs;
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
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(
IConfiguration configuration)
        {

            _configuration = configuration;
        }
        [HttpPost("Password")]
        public async Task<ActionResult<string>> Password([FromBody] UserInfo userInfo)
        {
            userInfo.Code = HttpContext.User.Identity.Name;
            if (userInfo.Password != userInfo.Password2) return BadRequest("La nueva contraseña no coincide.");
            var client = new RestClient(_configuration["path:API"] + "Password");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            userInfo.Mail = userInfo.User;
            request.AddJsonBody(userInfo);
            IRestResponse response = client.Execute(request);
            var res = response.Content;


            if (!res.Contains("Error"))
            {
                return "Ok";
            }
            else
            {
                return BadRequest("Fallo al querer loggearse");
            }
        }
    }
}

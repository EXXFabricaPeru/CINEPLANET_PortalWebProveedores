using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebProov_API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebProov_API.Data.Interfaces;

namespace WebProov_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private ILoginRepository _repo;
        private readonly IConfiguration _config;
        public PasswordController(ILoginRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Password([FromBody] User user)
        {
            var msj = _repo.ChangePassword(user);
            return Ok("Nuevo Password");
        }

 

    }

}

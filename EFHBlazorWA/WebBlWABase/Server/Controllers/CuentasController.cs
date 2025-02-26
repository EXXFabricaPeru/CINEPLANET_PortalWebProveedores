using EFHBlazzer.Server.WSEntities;
using EFHBlazzer.Shared.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFHBlazzer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public CuentasController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> Renovar()
        {
            var userInfo = new UserInfo()
            {
                User = HttpContext.User.Identity.Name
            };

            var usuario = await _userManager.FindByEmailAsync(userInfo.User);
            var roles = await _userManager.GetRolesAsync(usuario);

            return null;// BuildToken(userInfo, roles);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            //var result = await _signInManager.PasswordSignInAsync(userInfo.User, 
            //    userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            var client = new RestClient(_configuration["path:API"] + "Login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            userInfo.Mail = userInfo.User;
            request.AddJsonBody(userInfo);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = response.Content;
 

            if (!res.Contains("Error"))
            {
                res = res.Replace("\"", "");
                var token = new JwtSecurityToken(res);
                return BuildToken(token);
            }
            else
            {
                return BadRequest("Fallo al querer loggearse");
            }
        }

        [HttpPost("Password")]
        public async Task<ActionResult<string>> Password([FromBody] UserInfo userInfo)
        {
            var FSDFSDF = User;
            var ddd=HttpContext.User.Identity.Name;
            userInfo.User = User.FindFirst(JwtRegisteredClaimNames.UniqueName).Value;
            if (userInfo.Password!=userInfo.Password2) return BadRequest("La nueva contraseña no coincide.");
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

        private UserToken BuildToken(JwtSecurityToken tokenInfo)
        {
            var claims = new List<Claim>()
            {
        new Claim(JwtRegisteredClaimNames.UniqueName, tokenInfo.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
        new Claim(ClaimTypes.Name, tokenInfo.Claims.First(claim => claim.Type == ClaimTypes.Name).Value),
        new Claim(ClaimTypes.Email, tokenInfo.Claims.First(claim => claim.Type == ClaimTypes.Email).Value),
        new Claim("id", tokenInfo.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
        new Claim("ruc", tokenInfo.Claims.First(claim => claim.Type == ClaimTypes.Authentication).Value),
        new Claim("credit", tokenInfo.Claims.First(claim => claim.Type == ClaimTypes.Dns).Value),
        new Claim("token", tokenInfo.RawData),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            //foreach (var rol in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, rol));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(24);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        private UserToken BuildTokenOld(AuthUser userInfo)
        {
            var claims = new List<Claim>()
            {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.IdUsuario),
        new Claim(ClaimTypes.Name, userInfo.NombreApellido),
        new Claim(ClaimTypes.Hash, userInfo.Token),
        new Claim("id", userInfo.IdUsuario),
        new Claim("rol", userInfo.Rol),
        new Claim("token", userInfo.Token),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            //foreach (var rol in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, rol));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(24);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CorsoReact.Data;
using CorsoReact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CorsoReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _configuration;
        public LoginController(IUserService userService,IConfiguration configuration)
        {
            this._userService = userService;
            this._configuration = configuration;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Ciao, effettua il login per autenticarti");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<string> Post([FromBody]User userFromBody)
        {
            var user = this._userService.CheckUtente(userFromBody.Username, userFromBody.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Attenzione username e password inesistente" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return Ok(new User { Username = user.Username, Token = user.Token });

      
        }
    }
}
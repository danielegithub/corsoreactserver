using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CorsoReact.Data;
using CorsoReact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorsoReact.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AmiciController : ControllerBase
    {
        private IUserService _userService;
        public AmiciController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            var userFrom = this._userService.GetUtente(id.Value);
            return Ok(this._userService.GetUtenti(userFrom.Id));
        }

        [HttpGet("{param}")]
        public ActionResult<User> Get(string param)
        {
            return Ok(this._userService.GetUtente(param));
        }
    }
}
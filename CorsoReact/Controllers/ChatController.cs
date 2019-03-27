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
using Microsoft.Extensions.Caching.Memory;

namespace CorsoReact.Controllers
{
        [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        IUserService _userService;
        IDbConversazioni conversazioni;
        public ChatController(IUserService userService, IDbConversazioni conversazioni)
        {
            this._userService = userService;
            this.conversazioni = conversazioni;
        }

        [HttpGet]
        public ActionResult<Conversazione> Get(string idconversazione)
        {

            var conversazione = this.conversazioni.GetConversazione(idconversazione);
            if (conversazione == null)
            return BadRequest("Attenzione conversazione inesistente");
            else
            {
                return Ok(conversazione);
            }
        }

        [HttpPut]
        public ActionResult<Conversazione> Put(string idconversazione, string iduser, string testo)
        {
            var userFrom = this._userService.GetUtente(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userTo = this._userService.GetUtente(iduser);
            var messaggio = new Messaggio();
            messaggio.IdConversazione = idconversazione;
            messaggio.UserFrom = userFrom;
            messaggio.UserTo = userTo;
            messaggio.Testo = testo;
            this.conversazioni.AddMessaggio(idconversazione, messaggio);
            return Ok();
        }

        [HttpPost]
        public ActionResult<string> Post(string username,string testo)
        {
            var userFrom = this._userService.GetUtente(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userTo = this._userService.GetUtente(username);

            if (userTo == null)
            {
                return BadRequest("Attenzione utente To inesistente");
            }

            if (userFrom == null)
            {
                return BadRequest("Attenzione utente To inesistente");
            }
            var conversazione = this.conversazioni.AddConversazione();
            var messaggio = new Messaggio();
            messaggio.IdConversazione = conversazione.IdConversazione;
            messaggio.Testo = testo;
            messaggio.UserTo = userTo;
            conversazione.Messaggi.Add(messaggio);
            return Ok(new {  idconversazione = messaggio.IdConversazione });
        }

    
    }
}
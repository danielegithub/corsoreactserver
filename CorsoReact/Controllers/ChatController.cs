using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoReact.Data;
using CorsoReact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorsoReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        IUserService _userService;
        IMemoryCache conversazioni;
        public ChatController(IUserService userService, IMemoryCache cache)
        {
            this.conversazioni = cache;
            this._userService = userService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<Conversazione> Get([FromBody] string idconversazione)
        {
            var conversazione = this.conversazioni.Get<Conversazione>(idconversazione);
            if (conversazione == null)
            return BadRequest("Attenzione conversazione inesistente");
            else
            {
                return Ok(conversazione);
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<Conversazione> Put([FromBody] string idconversazione, string iduser, string testo)
        {
            var conversazione = this.conversazioni.Get<Conversazione>(idconversazione);
            if (conversazione == null)
                return BadRequest("Attenzione conversazione inesistente");
            else
            {
                return Ok(conversazione);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult<string> Post([FromBody] string iduser,string testo)
        {
            var userTo = this._userService.GetUtente(iduser);
            var userFrom = this._userService.GetUtente(User.FindFirst("NameIdentifier")?.Value);

            if (userTo == null)
            {
                return BadRequest("Attenzione utente To inesistente");
            }


            if (userFrom == null)
            {
                return BadRequest("Attenzione utente To inesistente");
            }

            var conversazione = new Conversazione();
            this.conversazioni.GetOrCreate(conversazione.IdConversazione, nuova =>
            {
                nuova.SlidingExpiration = TimeSpan.FromHours(10);
                return conversazione;
            });
            var messaggio = new Messaggio();
            messaggio.IdConversazione = conversazione.IdConversazione;
            messaggio.Testo = testo;
            messaggio.UserTo = userTo;
            conversazione.Messaggi.Add(messaggio);
            return Ok(new {  idconversazione = messaggio.IdConversazione });
        }

    
    }
}
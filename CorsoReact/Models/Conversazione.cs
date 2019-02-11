using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoReact.Models
{
    public class Conversazione
    {
        public Conversazione()
        {
            this.IdConversazione = Guid.NewGuid().ToString();
            this.Messaggi = new List<Messaggio>();
        }

        public List<Messaggio> Messaggi { get; set; }
        public string IdConversazione { get; set; }
    }
}

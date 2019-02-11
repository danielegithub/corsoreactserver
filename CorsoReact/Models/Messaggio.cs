using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoReact.Models
{
    public class Messaggio
    {
        public Messaggio()
        {
            this.DataMessaggio = DateTime.Now;
            this.IdMessaggio = Guid.NewGuid().ToString();
        }

        public string IdMessaggio { get; set; }
        public string IdConversazione { get; set; }
        public User UserFrom { get; set; }
        public User UserTo { get; set; }
        public string Testo { get; set; }
        public DateTime DataMessaggio { get; set; }
    }
}

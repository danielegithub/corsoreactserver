using CorsoReact.Models;
using System.Collections.Generic;
using System.Linq;

namespace CorsoReact.Data
{
    public interface IDbConversazioni
    {
        IEnumerable<Conversazione> GetConversazioni(string username);
        Conversazione GetConversazione(string idconversazione);
        Conversazione GetMessaggi(string idconversazione);
        Conversazione AddConversazione();
        void AddMessaggio(string idconversazione, Messaggio messaggio);
    }
    public class DbConversazioni : IDbConversazioni
    {
        public List<Conversazione> Conversazioni { get; set; }

        public DbConversazioni()
        {
            Conversazioni = new List<Conversazione>();
        }

        public IEnumerable<Conversazione> GetConversazioni(string username)
        {
            return Conversazioni.Where(conv => conv.Messaggi.Any(mes => mes.UserFrom.Username == username || mes.UserTo.Username == username));
        }

        public Conversazione GetMessaggi(string idconversazione)
        {
            return Conversazioni.FirstOrDefault(conv => conv.IdConversazione == idconversazione);
        }

        public void AddMessaggio(string idconversazione, Messaggio messaggio)
        {
            Conversazioni.FirstOrDefault(conv => conv.IdConversazione == idconversazione).Messaggi.Add(messaggio);
        }

        public Conversazione AddConversazione()
        {
            Conversazione conversazione = new Conversazione();
            Conversazioni.Add(conversazione);
            return conversazione;
        }

        public Conversazione GetConversazione(string idconversazione)
        {
            return Conversazioni.FirstOrDefault(conv => conv.IdConversazione == idconversazione);
        }
    }
}

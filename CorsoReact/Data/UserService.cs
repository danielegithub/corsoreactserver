using CorsoReact.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoReact.Data
{
    public interface IUserService
    {
        User CheckUtente(string username, string password);
        User GetUtente(string id);
        IEnumerable<User> GetUtenti(string id);
    }
    public class UserService : IUserService
    {
        IDbUtente dbUtente;
        public UserService(IDbUtente dbUtente)
        {
            this.dbUtente = dbUtente;
        }

       

        public User CheckUtente(string username, string password)
        {
            return this.dbUtente.GetUsers().FirstOrDefault(i => i.Username == username && i.Password == password);
        }

        public User GetUtente(string param)
        {
            return this.dbUtente.GetUsers().FirstOrDefault(i => i.Id == param || i.Username == param);
        }

        public IEnumerable<User> GetUtenti(string id)
        {
            return this.dbUtente.GetUsers().Where(i => i.Id != id);
        }
    }
}

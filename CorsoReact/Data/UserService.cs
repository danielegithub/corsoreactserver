using CorsoReact.Models;
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
    }
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Username = "pio@gmail.com", Password = "treglia" },
            new User { Username = "daniele@gmail.com", Password = "esposito" },
            new User { Username = "daniele@gmail.com", Password = "sisto" },
        };

        public User CheckUtente(string username, string password)
        {
            return this._users.FirstOrDefault(i => i.Username == username && i.Password == password);
        }

        public User GetUtente(string id)
        {
            return this._users.FirstOrDefault(i => i.Id == id);
        }
    }
}

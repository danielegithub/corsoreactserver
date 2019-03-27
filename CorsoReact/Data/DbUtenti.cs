using CorsoReact.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoReact.Data
{
    public interface IDbUtente
    {
        List<User> GetUsers();
    }
    public class DbUtente : IDbUtente
    {
        public List<User> Users { get; set; }

        public DbUtente()
        {
            this.Users = new List<User>
            {
                new User { Username = "pio@gmail.com", Password = "treglia" },
                new User { Username = "daniele@gmail.com", Password = "esposito" },
                new User { Username = "daniele@gmail.com", Password = "sisto" },
            };
        }

        public List<User> GetUsers()
        {
            return this.Users;
        }
    }
}

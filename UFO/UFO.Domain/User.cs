using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class User
    {
        public User()
        {
        }

        public User(string username, string password, string email, int roleId, int id = 0)
        {
            this.Id = id;
            this.RoleId = roleId;
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}

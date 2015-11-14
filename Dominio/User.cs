using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        
        public User(string name, string password, string email, string lastName)
        {
            this.Email = email;
            this.Name = name;
            this.Password = password;
            this.LastName = lastName;
        }

        public User(string email)
        {
            this.Email = email;
        }
    }
}

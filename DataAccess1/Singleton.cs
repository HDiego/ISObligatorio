using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Singleton
    {
        private static Singleton instance;
        List<User> listUsers;
        //"Hola"
        private Singleton()
        {
            listUsers = new List<User>();
            AddUsers();
        }

        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public void AddUsers()
        {
            listUsers.Add(new User("Diego", "1234", "diego@ort.com", "Rocca"));
            listUsers.Add(new User("Mauricio", "1234", "mauri@ort.com", "Delbono"));
            listUsers.Add(new User("Gerardo", "1234", "gerardo@ort.com", "Quintana"));
        }

        public User GetUser(string email)
        {
            foreach (User u in listUsers)
            {
                if (u.Email.Equals(email))
                    return u;
            }
            return null;
        }

        public bool DeleteUser(string email)
        {
            User u = new User(email);
            if (listUsers.Contains(u))
            {
                listUsers.Remove(u);
                return true;
            }
            return false;
        }
    }
}

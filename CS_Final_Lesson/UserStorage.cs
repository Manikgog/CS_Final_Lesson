using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Final_Lesson
{
    public class UserStorage : IUserStorage
    {
        public event action UserAdded;
        public event action UserDeleted;
        public List<User> Users { get; set; }

        public UserStorage()
        {
            Users = new List<User>();
        }
        
        public int Count()
        {
            return Users.Count;
        }

        public User this[int index]
        {
            get
            {
                return Users[index];
            }

            set
            {
                Users[index] = value;
            }
        }
        public void AddUser(User user)
        {
            Users.Add(user);
            UserAdded?.Invoke(user);
        }
        public User GetUser(int id)
        {
            User u = Users.Where(user => user.Id == id).FirstOrDefault();
            return u;
        }
        public void UpdateUser(int id, User user)
        {
            User u = Users.Where(user_ => user_.Id == id).FirstOrDefault();
            u.Email = user.Email;
            u.Username = user.Username;
            u.Id = user.Id;
        }
        public void DeleteUser(int id)
        {
            UserDeleted?.Invoke(GetUser(id));
            Users.Remove(GetUser(id));
            
        }

        public override string ToString()
        {
            string users = "";

            foreach(User user in Users)
            {
                users += user.ToString() + "\n";
            }

            return users;
        }
    }
}

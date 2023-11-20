using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Final_Lesson
{
    public interface IUserStorage
    {
        void AddUser(User user);
        User GetUser(int id);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}

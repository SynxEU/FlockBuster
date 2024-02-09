using Flockbuster.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Service.Interfaces
{
    public interface IUser
    {
        List<Users> GetUsers();
        Users GetUsersById(int id);
        Users GetUserByMail(string mail);
        Users GetUserDetailsLogin(string mail, string password);
        Users CreateUser(string name, int age, string mail, string password);
        void DeleteUser(int id);
        Users UpdateUser(int id, string name, int age, string mail);
        Users UpdateUserPassword(int id, string password);
        Users Admin(int id, bool admin);
        void UpdateUserPicture(int id, string filePath);
        bool CheckPassowrd(int id, string password);
    }
}

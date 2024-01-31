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
        Users GetUserIdLogin(string mail, string password);
        Users CreateUser(string name, int age, string mail, string password);
    }
}

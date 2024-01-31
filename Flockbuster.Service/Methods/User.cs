using Flockbuster.Domain.Models;
using Flockbuster.Domain;
using Flockbuster.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Service.Methods
{
    public class User : IUser
    {
        SQLConn _connection;

        public User(IConfiguration configuration) => _connection = new SQLConn(configuration);

        public List<Users> GetUsers() => _connection.GetUsers();
        public Users GetUserByMail(string mail) => _connection.GetUserByMail(mail);
        public Users GetUsersById(int id) => _connection.GetUsersById(id);
        public Users GetUserIdLogin(string mail, string password) => _connection.GetUserIdLogin(mail, password);
        public Users CreateUser(string name, int age, string mail, string password) => _connection.CreateUser(name, age, mail, password);
    }
}

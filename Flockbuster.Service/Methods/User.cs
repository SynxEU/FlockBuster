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
        public Users GetUserDetailsLogin(string mail, string password) => _connection.GetUserDetailsLogin(mail, password);
        public Users CreateUser(string name, int age, string mail, string password) => _connection.CreateUser(name, age, mail, password);
        public void DeleteUser(int id) => _connection?.DeleteUser(id);
        public Users UpdateUserPassword(int id, string password) => _connection.UpdateUserPassword(id, password);
        public Users UpdateUser(int id, string name, int age, string mail) => _connection.UpdateUser(id, name, age, mail);
        public bool Admin(int id, bool admin) => _connection.Admin(id, admin);
        public void UpdateUserPicture(int id, string filePath) => _connection.UpdateUserPicture(id, filePath);
        public bool CheckPassowrd(int id, string password) => _connection.CheckPassowrd(id, password);
    }
}

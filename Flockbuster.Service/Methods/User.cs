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

        public User(IConfiguration configuration) { _connection = new SQLConn(configuration); }

        public List<Users> GetUsers() => _connection.GetUsers();
    }
}

using Flockbuster.Domain;
using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Service.Methods
{
    public class Movie : IMovie
    {
        SQLConn _connection;

        public Movie(IConfiguration configuration) { _connection = new SQLConn(configuration); }

        public List<Movies> GetMovies() => _connection.GetMovies();
    }
}

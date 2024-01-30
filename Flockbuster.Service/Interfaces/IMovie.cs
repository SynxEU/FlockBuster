using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flockbuster.Domain.Models;

namespace Flockbuster.Service.Interfaces
{
    public interface IMovie
    {
        List<Movies> GetMovies();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Domain.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int RequiredAge { get; set; }
        public long TTW { get; set; }
        public string RelaseDate { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<Genre> Genres { get; set; }
    }
}

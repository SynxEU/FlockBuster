using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flockbuster.Domain.Models
{
    public class BorrowedMovie
    {
        public int Id { get; set; }
        public int MovieID { get; set; }
        public int UserID { get; set; }
        public bool IsBorrowed { get; set; }
        public bool WasBorrowed { get; set; }
        public string UserName { get; set; }
        public string MovieName { get; set; }
        public List<Genre> BorrowedMoviesGenres { get; set;}
    }
}

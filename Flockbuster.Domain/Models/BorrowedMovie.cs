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
        public Movies MovieID { get; set; }
        public Users UserID { get; set; }
        public bool IsBorrowed { get; set; }
        public bool WasBorrowed { get; set; }
    }
}

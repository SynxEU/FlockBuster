using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages.Admin
{
    public class MovieHistoryModel : PageModel
    {
        private readonly IMovie _movie;
        private readonly IUser _user;

        public MovieHistoryModel(IMovie movie, IUser user)
        {
            _movie = movie;
            _user = user;
        }
        [BindProperty]
        public List<BorrowedMovie> BorrowedMovies { get; set; } = new List<BorrowedMovie>();
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || !HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/error/403");
            }
            else
            {
                BorrowedMovies = _movie.GetBorrowedMovies();

                foreach (BorrowedMovie borrowed in BorrowedMovies)
                {
                    borrowed.MovieName = _movie.GetMoviesById(borrowed.MovieID).Title;
                    borrowed.UserName = _user.GetUsersById(borrowed.UserID).Name;
                }

                return Page();
            }
        }
    }
}

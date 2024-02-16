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
        [BindProperty]
        public List<Users> User { get; set; } = new List<Users>();
        [BindProperty]
        public List<Movies> Movie { get; set; } = new List<Movies>();
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
                    User.Add(new Users
                    {
                        Name = _user.GetUsersById(borrowed.UserID).Name
                    });
                    Movie.Add(new Movies
                    {
                        Title = _movie.GetMoviesById(borrowed.MovieID).Title
                    });
                }

                foreach (Movies mov in Movie)
                {
                    List<Genre> genreForMovies = _movie.GetMovieGenre(mov.Id);
                    mov.Genres = genreForMovies;
                }

                return Page();
            }
        }
    }
}

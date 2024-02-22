using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages
{
    public class MoviesModel : PageModel
    {
        private readonly IMovie _movie;
        private readonly IUser _user;
        public MoviesModel(IMovie movie, IUser user)
        {
            _movie = movie;
            _user = user;
        }
        [BindProperty]
        public List<Movies> ListOfMovies { get; set; } = new List<Movies>();
        public void OnGet()
        {
            ListOfMovies = _movie.GetMovies();
            foreach (Movies mov in ListOfMovies)
            {
                List<Genre> genreForMovies = _movie.GetMovieGenre(mov.Id);
                mov.Genres = genreForMovies;
            }
        }
        public IActionResult OnPostBorrow(int movieId)
        {
            int? tempid = HttpContext.Session.GetInt32("ID");
            int id = tempid.HasValue ? tempid.Value : 0;

            int balance = _user.GetUsersById(id).Balance;
            int price = _movie.GetMoviesById(movieId).Price;

            if (balance >= price)
            {
                _movie.BorrowMovie(movieId, id);
                _user.UpdateUserBalanceMinus(id, price);
                return RedirectToPage("/User/Dashboard");
            }
            else
            {
                ModelState.AddModelError("Funds", "No funds");
                return RedirectToPage("/Movies");
            }
        }
    }
}

using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Flockbuster.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages.Admin
{
    public class MoviesModel : PageModel
    {
        private readonly IMovie _movie;
        public MoviesModel(IMovie movie) 
        { 
            _movie = movie;
        }
        [BindProperty]
        public List<Movies> ListOfMovies { get; set; } = new List<Movies>();
        [BindProperty]
        public List<Genre> ListOfGenres { get; set; } = new List<Genre>();
        public IActionResult OnGet()
        {
            ListOfMovies = _movie.GetMovies();
            foreach (Movies mov in ListOfMovies)
            {
                List<Genre> genreForMovies = _movie.GetMovieGenre(mov.Id);
                mov.Genres = genreForMovies;
            }
            if (!HttpContext.Session.GetInt32("ID").HasValue || !HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }
        }
    }
}

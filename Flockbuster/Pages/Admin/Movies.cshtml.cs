using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Flockbuster.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Flockbuster.Service.Methods;

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
                return RedirectToPage("/error/403");
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPostEdit(int movieId)
        {
            HttpContext.Session.SetInt32("TempMovieID", movieId);
            return RedirectToPage("/Admin/Edit/Movie");
        }
        public IActionResult OnPostCreate()
        {
            return RedirectToPage("/Admin/Create/Movie");
        }
        public IActionResult OnPostDelete(int movieId)
        {
            _movie.DeleteMovie(movieId);
            return RedirectToPage("/Admin/Movies");
        }
    }
}

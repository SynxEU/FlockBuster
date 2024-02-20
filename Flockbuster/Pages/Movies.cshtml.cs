using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages
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
        public void OnGet()
        {
            ListOfMovies = _movie.GetMovies();
            foreach (Movies mov in ListOfMovies)
            {
                List<Genre> genreForMovies = _movie.GetMovieGenre(mov.Id);
                mov.Genres = genreForMovies;
            }
        }
        
    }
}

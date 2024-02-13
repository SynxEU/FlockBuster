using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Flockbuster.Service.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace Flockbuster.Pages.Admin.Edit
{
    public class MovieModel : PageModel
    {
        private readonly IMovie _movie;

        public MovieModel(IMovie movie)
        {
            _movie = movie;
        }
        [BindProperty]
        public Movies Movie { get; set; } = new Movies();

        [BindProperty]
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        public string AgeRating { get; set; } = string.Empty;

        [BindProperty, DisplayName("Movie Picture")]
        public IFormFile Img { get; set; }
        [BindProperty]
        public string Hours { get; set; } = string.Empty;

        [BindProperty]
        public string Miuntes { get; set; } = string.Empty;

        [BindProperty]
        public DateTime ReleaseDate { get; set; }

        [BindProperty]
        public string Price { get; set; } = string.Empty;

        [BindProperty]
        public string AdminPassword { get; set; } = string.Empty;
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || !HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/error/403");
            }
            else
            {
                int? tempid = HttpContext.Session.GetInt32("TempMovieId");
                int id = tempid.HasValue ? tempid.Value : 0;
                Movie = _movie.GetMoviesById(id);

                return Page();
            }
        }
    }
}

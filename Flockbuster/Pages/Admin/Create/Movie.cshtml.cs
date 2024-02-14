using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Flockbuster.Service.Methods;

namespace Flockbuster.Pages.Admin.Create
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
        public List<Genre> Genres { get; set; } = new List<Genre>();

        [BindProperty]
        public string Title { get; set; } = string.Empty;

        [BindProperty]
        public int AgeRating { get; set; }

        [BindProperty, DisplayName("Movie Picture")]
        public IFormFile Img { get; set; }
        [BindProperty]
        public int Hours { get; set; }

        [BindProperty]
        public int Miuntes { get; set; }

        [BindProperty]
        public DateTime ReleaseDate { get; set; }

        [BindProperty]
        public int GenreId { get; set; }

        [BindProperty]
        public int Price { get; set; }

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
                Genres = _movie.GetGenres();
                return Page();
            }
        }
        public IActionResult OnPostCancel() => RedirectToPage("/Admin/Movies");
        public IActionResult OnPostCreate()
        {
            if (AdminPassword != "Admin1234!" || ReleaseDate <= DateTime.MinValue || ReleaseDate >= DateTime.MaxValue || ReleaseDate <= DateTime.Now.Date || Title == null || Hours == 0 || Miuntes == 0 || Price == 0 || GenreId == 0) { return Page(); }

            _movie.CreateMovie(Title, AgeRating, Hours, Miuntes, ReleaseDate, Price, GenreId);

            if (Img != null && Img.Length > 0)
            {
                int id = _movie.GetMovieByTitle(Title);
                string uniqueFileName = $"id{id}_{Img.FileName}";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pics", "Movies", uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create)) { Img.CopyTo(fileStream); }

                string relativeFilePath = $"/pics/Movies/{uniqueFileName}";
                _movie.UpdateMoviePicture(id, relativeFilePath);
            }

            return RedirectToPage("~/Admin/Movies");
        }
    }
}

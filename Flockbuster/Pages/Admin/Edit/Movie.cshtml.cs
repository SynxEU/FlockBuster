using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Flockbuster.Service.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public int AgeRating { get; set; } 

        [BindProperty, DisplayName("Movie Picture")]
        public IFormFile Img { get; set; }
        [BindProperty]
        public int Hours { get; set; } 

        [BindProperty]
        public int Miuntes { get; set; } 

        [BindProperty, DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

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
                int? tempid = HttpContext.Session.GetInt32("TempMovieID");
                int id = tempid.HasValue ? tempid.Value : 0;
                Movie = _movie.GetMoviesById(id);

                ReleaseDate = Movie.RelaseDate;

                return Page();
            }
        }
        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("TempMovieId");
            return RedirectToPage("/Admin/Movies");
        }
        public IActionResult OnPostUpdate()
        {
            if (AdminPassword == "Admin1234!")
            {
                int? tempid = HttpContext.Session.GetInt32("TempMovieID");
                int id = tempid.HasValue ? tempid.Value : 0;

                _movie.UpdateMovie(id, Title, AgeRating, Hours, Miuntes, ReleaseDate, Price);

                if (Img != null && Img.Length > 0)
                {
                                        
                    string uniqueFileName = $"id{id}{Path.GetExtension(Img.FileName)}";
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pics", "Movies", uniqueFileName);

                    if (Movie.Img == filePath)
                    {
                        Directory.Delete(filePath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) { Img.CopyTo(fileStream); }

                    string relativeFilePath = $"/pics/Movies/{uniqueFileName}";
                    _movie.UpdateMoviePicture(id, relativeFilePath);
                }

                HttpContext.Session.Remove("TempMovieID");
                return RedirectToPage("/Admin/Movies");
            }
            else
            {
                HttpContext.Session.Remove("TempMovieID");
                return RedirectToPage("/Admin/Movies");
            }
        }
    }
}

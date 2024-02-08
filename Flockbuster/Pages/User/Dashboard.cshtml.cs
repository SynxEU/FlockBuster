using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;

namespace Flockbuster.Pages.User
{
    public class DashboardModel : PageModel
    {
		private readonly IUser _user;
		private readonly IMovie _movie;

		public DashboardModel(IUser user, IMovie movie)
		{
			_user = user;
			_movie = movie;
		}
		[BindProperty]
		public Users UserDetails { get; set; }
		[BindProperty]
		public List<Movies> ListOfMovies { get; set; } = new List<Movies>();
		public IActionResult OnGet()
		{
			if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
			{
				return RedirectToPage("/error/403");
			}
			else
			{
				int? tempId = HttpContext.Session.GetInt32("ID");
				int id = tempId.HasValue ? tempId.Value : 0;
				UserDetails = _user.GetUsersById(id);

				BorrowedMovie loanedMovie = new BorrowedMovie();
				loanedMovie = _movie.GetBorrowedMovieById(id);

				Movies borrowedMovie = new Movies();
				borrowedMovie = _movie.GetMoviesById(loanedMovie.MovieID);

				if (loanedMovie.Id != 0 && loanedMovie.IsBorrowed)
				{
					ListOfMovies.Add(borrowedMovie);
				}
				foreach (Movies mov in ListOfMovies)
				{
					List<Genre> genresForMovie = _movie.GetMovieGenre(loanedMovie.MovieID);
					mov.Genres = genresForMovie;
				}

				return Page();
			}
		}
	}
}

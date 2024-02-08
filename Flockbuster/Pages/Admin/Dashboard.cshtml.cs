using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;

namespace Flockbuster.Pages.Admin
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
		public List<BorrowedMovie> ListOfBorrowedMovies { get; set; } = new List<BorrowedMovie>();
		[BindProperty]
		public List<Movies> ListOfMovies { get; set; } = new List<Movies>();
		[BindProperty]
		public List<Users> ListOfUsers { get; set; } = new List<Users>();
		[BindProperty]
		public List<Genre> ListOfGenres { get; set; } = new List<Genre>();

		public IActionResult OnGet()
		{
			ListOfUsers = _user.GetUsers();
			ListOfUsers.RemoveAt(0);
			ListOfMovies = _movie.GetMovies();
			ListOfBorrowedMovies = _movie.GetBorrowedMovies();
			ListOfGenres = _movie.GetGenres();
			if (!HttpContext.Session.GetBoolean("Admin"))
			{
				return RedirectToPage("/error/403");
			}
			else
			{
				return Page();
			}

		}
	}
}

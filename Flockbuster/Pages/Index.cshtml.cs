using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Flockbuster.Service.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
		private readonly IMovie _movie;
        private readonly IUser _user;

		public IndexModel(ILogger<IndexModel> logger, IMovie movie, IUser user)
		{
            _logger = logger;
			_movie = movie;
            _user = user;
		}

		[BindProperty]
		public List<Movies> ListOfMovies { get; set; } = new List<Movies>();
        [BindProperty]
        public List<Movies> ThreeNewestMovies { get; set; } = new List<Movies>();
        public void OnGet()
        {
            List<Movies> templist = new List<Movies>();
            ListOfMovies = _movie.GetMovies();
            templist = ListOfMovies.ToList();
            templist.Reverse();
            ThreeNewestMovies = templist.Take(3).ToList();
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
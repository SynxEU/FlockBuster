using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Flockbuster.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Flockbuster.Service.Methods;

namespace Flockbuster.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly IUser _user;
        public UsersModel(IUser user)
        {
            _user = user;
        }
        [BindProperty]
        public List<Users> ListOfUsers { get; set; } = new List<Users>();
        public IActionResult OnGet()
        {
            ListOfUsers = _user.GetUsers();
            ListOfUsers.RemoveAt(0);

            if (!HttpContext.Session.GetInt32("ID").HasValue || !HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/errors/403");
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPostEdit(int userId)
        {
            HttpContext.Session.SetInt32("TempUserId", userId);
            return RedirectToPage("/Admin/Edit/User");
        }
        public IActionResult OnPostDelete(int userId)
        {
            _user.DeleteUser(userId);
            return RedirectToPage("/Admin/Users");
        }
    }
}

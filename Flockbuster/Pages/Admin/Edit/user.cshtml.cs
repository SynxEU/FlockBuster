using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Flockbuster.Pages.Admin.Edit
{
    public class UserModel : PageModel
    {
        private readonly IUser _user;
        public UserModel(IUser user)
        {
            _user = user;
        }

        [BindProperty]
        public Users User { get; set; } = new Users();

        [BindProperty]
        public string UserFirstName { get; set; } = string.Empty;

        [BindProperty]
        public string UserLastName { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string EmailAddress { get; set; } = string.Empty;

        [BindProperty]
        public string Age { get; set; } = string.Empty;

        [BindProperty]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;
        [BindProperty]
        public string AdminPassword { get; set; } = string.Empty;
        [BindProperty]
        public bool IsChecked { get; set; }

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || !HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/error/403");
            }
            else
            {
                int? tempid = HttpContext.Session.GetInt32("TempUserId");
                int id = tempid.HasValue ? tempid.Value : 0;
                User = _user.GetUsersById(id);

                string[] names = User.Name.Split(' ');
                UserFirstName = names[0];
                UserLastName = names[1];

                return Page();
            }
        }

        public IActionResult OnPostCancel()
        {
            HttpContext.Session.Remove("TempUserId");
            return RedirectToPage("/Admin/Users");
        }
        public IActionResult OnPostUpdate()
        {
            int? tempid = HttpContext.Session.GetInt32("TempUserId");
            int id = tempid.HasValue ? tempid.Value : 0;
            string fullName = FirstName + " " + LastName;
            if (AdminPassword == "Admin1234!")
            {
                if (!string.IsNullOrEmpty(Password) && Password == ConfirmPassword) { _user.UpdateUserPassword(id, ConfirmPassword); }
                if (IsChecked) { _user.Admin(id, true); }
                else { _user.Admin(id, false); }
                _user.UpdateUser(id, fullName, Convert.ToInt32(Age), EmailAddress);
                HttpContext.Session.Remove("TempUserId");
                return RedirectToPage("/Admin/Users");
            }
            else { return RedirectToAction("adminInput"); }
        }
    }
}

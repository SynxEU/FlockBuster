using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Flockbuster.Pages
{
    public class SigninModel : PageModel
    {
        private readonly IUser _user;
        public SigninModel(IUser user)
        {
            _user = user;
        }

        public void OnGet()
        {
        }
        [BindProperty, Required]
        public string Mail { get; set; } = string.Empty;
        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;
        public IActionResult OnPost()
        {
            Users user = new Users();
            user.Email = Mail;

            if (string.IsNullOrWhiteSpace(Password)) { ModelState.AddModelError("asp", "Wrong password"); }
            else { user = _user.GetUserDetailsLogin(user.Email, Password); }

            if (string.IsNullOrEmpty(user.Name)) { ModelState.AddModelError("asp", "Wrong password or email"); }

            if (ModelState.IsValid)
            {
                HttpContext.Session.Boolean("Admin", user.IsAdmin);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetInt32("ID", user.Id);
                if (user.IsAdmin == true) { return RedirectToPage("/Admin/Dashboard"); }
                else { return RedirectToPage("/user/dashboard"); }
            }
            else { return Page(); }
        }
    }
}

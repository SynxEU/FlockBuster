using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Flockbuster.Pages
{
    public class SignupModel : PageModel
    {
        private readonly IUser _user;

        public SignupModel(IUser user)
        {
            _user = user;
        }

        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string EmailAddress { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Age { get; set; } = string.Empty;

        [BindProperty, Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [BindProperty, Required]
        [Compare(nameof(Password), ErrorMessage = "Make sure passwords are matching")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            string fullName = FirstName + " " + LastName;

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                _user.CreateUser(fullName, Convert.ToInt32(Age), EmailAddress, ConfirmPassword);
                Users user = new Users();

                user.Email = EmailAddress;
                user = _user.GetUserByMail(user.Email);

                HttpContext.Session.Boolean("Admin", user.IsAdmin);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetInt32("ID", user.Id);
                if (user.IsAdmin) { return RedirectToPage("/admin/dashboard"); }
                else { return RedirectToPage("/user/dashboard"); }
            }

            else
            {
                ModelState.AddModelError("Signup", "Details don't look right");
                return Page();
            }

        }
    }
}

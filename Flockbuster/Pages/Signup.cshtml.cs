using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        [MinLength(7)]
        public string Age { get; set; } = string.Empty;

        [BindProperty, Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [BindProperty, Required]
        [Compare(nameof(Password), ErrorMessage = "Make sure passwords are matching")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

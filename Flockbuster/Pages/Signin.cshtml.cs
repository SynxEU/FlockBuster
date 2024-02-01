using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Flockbuster.Pages
{
    public class SigninModel : PageModel
    {
        public void OnGet()
        {
        }
        [BindProperty, Required]
        public string Mail { get; set; } = string.Empty;
        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Flockbuster.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string FullName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Subject { get; set; } = string.Empty;

        [BindProperty, MaxLength(500)]
        public string Textarea { get; set; } = string.Empty;

        public void OnGet()
        {
        }
        public IActionResult OnPostContact()
        {
            Console.WriteLine($"Fullname: {FullName}\nEmail: {Email}\nSubject: {Subject}\n\n{Textarea}");
            return RedirectToPage("/Contact");
        }
    }
}

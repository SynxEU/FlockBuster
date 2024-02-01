using Flockbuster.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Reflection.Metadata.BlobBuilder;

namespace Flockbuster.Pages.User
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
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

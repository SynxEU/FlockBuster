using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages.Admin.Edit
{
    public class MovieModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || !HttpContext.Session.GetBoolean("Admin"))
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

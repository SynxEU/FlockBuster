using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flockbuster.Pages
{
    public class SignoutModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPostDontSignOut()
        {
            if (HttpContext.Session.GetBoolean("Admin") == false) { return RedirectToPage("/User/dashboard"); }
            else { return RedirectToPage("/Admin/Dashboard"); }
        }
        public IActionResult OnPostSignOut()
        {
            HttpContext.Session.Remove("Admin");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("ID");
            return RedirectToPage("/Index");
        }
    }
}

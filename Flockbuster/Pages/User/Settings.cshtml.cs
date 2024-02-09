using Flockbuster.Domain.Models;
using Flockbuster.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace Flockbuster.Pages.User
{
    public class SettingsModel : PageModel
    {
        private readonly IUser _user;

        public SettingsModel(IUser user)
        {
            _user = user;
        }
        [BindProperty]
        public Users User { get; set; } = new Users();
        
        [BindProperty]
        public string UserFirstName { get; set; } = string.Empty;

        [BindProperty]
        public string UserLastName { get; set; } = string.Empty;

        [BindProperty, DisplayName("Profile Picture")]
        public IFormFile Img { get ; set; }
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
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("ID").HasValue || HttpContext.Session.GetBoolean("Admin"))
            {
                return RedirectToPage("/error/403");
            }
            else
            {
                int? tempId = HttpContext.Session.GetInt32("ID");
                int id = tempId.HasValue ? tempId.Value : 0;
                User = _user.GetUsersById(id);

                string[] names = User.Name.Split(' ');
                UserFirstName = names[0];
                UserLastName = names[1];

                return Page();
            }
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/User/Dashboard");
        }
        public IActionResult OnPostUpdate()
        {
            int? tempId = HttpContext.Session.GetInt32("ID");
            int id = tempId.HasValue ? tempId.Value : 0;
            string fullName = FirstName + " " + LastName;
            bool currentPassword = _user.CheckPassowrd(id, CurrentPassword);

            if (!currentPassword)
            {
                ModelState.AddModelError("password", "Not the correct password");
                return RedirectToPage("/User/Settings");
            }
            else
            {
                if (!string.IsNullOrEmpty(Password) && Password == ConfirmPassword)
                {
                    _user.UpdateUserPassword(id, Password);
                }
                if (Img != null && Img.Length > 0)
                {
                    string uniqueFileName = "id" + id + "_" + Img.FileName;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pics", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Img.CopyTo(fileStream);
                    }

                    string relativeFilePath = "/pics/" + uniqueFileName;
                    _user.UpdateUserPicture(id, relativeFilePath);
                }
                _user.UpdateUser(id, fullName, Convert.ToInt32(Age), EmailAddress);
                HttpContext.Session.SetString("Name", fullName);
                return RedirectToPage("/User/Dashboard");
            }
        }
        public IActionResult OnPostDelete()
        {
            int? tempId = HttpContext.Session.GetInt32("ID");
            int id = tempId.HasValue ? tempId.Value : 0;
            bool currentPassword = _user.CheckPassowrd(id, CurrentPassword);

            if (currentPassword)
            {
                HttpContext.Session.Remove("Admin");
                HttpContext.Session.Remove("Name");
                HttpContext.Session.Remove("ID");
                _user.DeleteUser(id);
                return RedirectToPage("/index");
            }
            else
            {
                return Page();
            }
        }
    }
}
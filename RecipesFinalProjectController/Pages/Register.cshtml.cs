using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Diagnostics.Contracts;

namespace RecipesFinalProjectController.Pages
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Profile");

            return Page();
        }
        public IActionResult OnPost()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Profile");
            try
            {
                if (string.IsNullOrWhiteSpace(FirstName) ||
                    string.IsNullOrWhiteSpace(LastName) ||
                    string.IsNullOrWhiteSpace(Username) ||
                    string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please fill in all fields.";
                    return Page();
                }

                Users user = new Users
                {
                    FirstName = FirstName.Trim(),
                    LastName = LastName.Trim(),
                    Username = Username.Trim(),
                    Password = Password,
                    IsAdmin = false
                };

                UsersService.Create(user);

                TempData["Message"] = "Account created successfully. You can now login.";

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}

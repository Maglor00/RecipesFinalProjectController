using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Users user = UsersService.Login(UserName, Password);

            if (user == null)
            {
                ErrorMessage = "Invalid login";
                return Page();
            }

            HttpContext.Session.SetString(
                "LoggedInUser",
                JsonSerializer.Serialize(user)
                );

            return RedirectToPage("/Profile");
        }
    }
}

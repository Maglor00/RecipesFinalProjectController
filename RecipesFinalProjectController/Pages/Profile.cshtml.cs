using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class ProfileModel : PageModel
    {
        public Users User { get; set; }

        public IActionResult OnGet()
        {
            string userJson = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login");
            }

            User = JsonSerializer.Deserialize<Users>(userJson);

            return Page();
        }
    }
}

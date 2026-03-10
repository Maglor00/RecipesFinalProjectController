using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class ProfileModel : PageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            FirstName = User.FindFirst("FirstName")?.Value;
            LastName = User.FindFirst("LastName")?.Value;

            return Page();
        }
    }
}

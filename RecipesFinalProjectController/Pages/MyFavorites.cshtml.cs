using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;

namespace RecipesFinalProjectController.Pages
{
    public class MyFavoritesModel : PageModel
    {
        public List<Recipes> Favorites { get; set; } = new();

        public IActionResult OnGet()
        {
            int? userId =
                HttpContext.Session.GetInt32("LoggedInUserId");

            if (userId == null)
                return RedirectToPage("/Login");

            Favorites =
                FavoritesService.GetUserFavorites(userId.Value);

            return Page();
        }
    }
}

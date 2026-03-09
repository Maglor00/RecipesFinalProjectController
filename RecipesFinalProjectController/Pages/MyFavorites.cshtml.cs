using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;

namespace RecipesFinalProjectController.Pages
{
    public class MyFavoritesModel : PageModel
    {
        public List<Recipes> Favorites { get; set; } = new();

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Favorites =
                FavoritesService.GetUserFavorites(userId);

            return Page();
        }
    }
}

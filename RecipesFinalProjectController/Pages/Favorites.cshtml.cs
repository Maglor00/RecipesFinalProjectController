using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices.Interface;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesModel(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        public List<Recipes> Favorites { get; set; }
        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("LoggedInUser");

            if (userJson == null)
                return RedirectToPage("/Login");

            var user = JsonSerializer.Deserialize<Users>(userJson);

            Favorites = _favoritesService.GetUserFavorites(user.Id);

            return Page();
        }
    }
}

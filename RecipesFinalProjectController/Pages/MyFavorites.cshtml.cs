using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices.Interface;

namespace RecipesFinalProjectController.Pages
{
    public class MyFavoritesModel : PageModel
    {
        private readonly IFavoritesService _favoritesService;

        public MyFavoritesModel(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }
        public List<Recipes> Favorites { get; set; } = new();

        public IActionResult OnGet()
        {
            int? userId =
                HttpContext.Session.GetInt32("LoggedInUserId");

            if (userId == null)
                return RedirectToPage("/Login");

            Favorites =
                _favoritesService.GetUserFavorites(userId.Value);

            return Page();
        }
    }
}

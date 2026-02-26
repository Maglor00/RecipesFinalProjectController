using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using RecipesFinalProjectServices;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class SearchModel : PageModel
    {
        

        public List<Recipes> Recipes { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Difficulty> Difficulties { get; set; } = new();
        public List<int> FavoriteRecipeIds { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? DifficultyId { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? MaxTime { get; set; }

        public bool IsLoggedIn =>
            HttpContext.Session.GetInt32("LoggedInUserId") != null;

        public void OnGet()
        {
            Categories = CategoryService.RetrieveAll();
            Difficulties = DifficultyService.RetrieveAll();

            Recipes = RecipesService.Search(
                Title,
                CategoryId,
                DifficultyId,
                MaxTime);

            if (IsLoggedIn)
            {
                int userId =
                    HttpContext.Session.GetInt32("LoggedInUserId").Value;

                var favorites =
                    FavoritesService.GetUserFavorites(userId);

                FavoriteRecipeIds =
                    favorites.Select(r => r.Id).ToList();
            }
        }

        public IActionResult OnPostToggleFavorite(int recipeId)
        {
            int? userId =
                HttpContext.Session.GetInt32("LoggedInUserId");

            if (userId == null)
                return RedirectToPage("/Login");

            if (FavoritesService.IsFavorite(userId.Value, recipeId))
                FavoritesService.RemoveFavorite(userId.Value, recipeId);
            else
                FavoritesService.AddFavorites(userId.Value, recipeId);

            return RedirectToPage();
        }
    }
}

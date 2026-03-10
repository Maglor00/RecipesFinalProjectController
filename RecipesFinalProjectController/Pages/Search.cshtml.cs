using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using RecipesFinalProjectServices;
using System.Security.Claims;
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

        public bool IsLoggedIn => User.Identity.IsAuthenticated;
            

        public void OnGet()
        {
            Categories = CategoryService.RetrieveAll();
            Difficulties = DifficultyService.RetrieveAll();

            Recipes = RecipesService.Search(
                Title,
                CategoryId,
                DifficultyId,
                MaxTime);

            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var favorites =
                    FavoritesService.GetUserFavorites(userId);

                FavoriteRecipeIds =
                    favorites.Select(r => r.Id).ToList();
            }
        }

        public IActionResult OnPostToggleFavorite(int recipeId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (FavoritesService.IsFavorite(userId, recipeId))
                FavoritesService.RemoveFavorite(userId, recipeId);
            else
                FavoritesService.AddFavorites(userId, recipeId);

            return RedirectToPage(new
            {
                Title,
                CategoryId,
                DifficultyId,
                MaxTime
            });
        }
    }
}

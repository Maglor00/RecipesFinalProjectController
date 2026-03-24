using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;

namespace RecipesFinalProjectController.Pages
{
    public class IndexModel : PageModel
    {
        private const int NumberOfTopRecipes = 5;
       

        public List<Recipes> TopRecipes { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        
        public void OnGet()
        {
            Categories = CategoryService.RetrieveAll();
            TopRecipes = RecipesService.RetrieveTopRecipes(NumberOfTopRecipes);

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var favoriteIds = FavoritesService
                    .GetUserFavorites(userId)
                    .Select(r => r.Id)
                    .ToHashSet();

                foreach (var recipe in TopRecipes)
                {
                    recipe.IsFavorite = favoriteIds.Contains(recipe.Id);
                }
            }
        }
    }
}

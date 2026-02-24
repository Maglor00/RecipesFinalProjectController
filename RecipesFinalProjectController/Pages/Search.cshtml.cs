using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices.Interface;
using System.Text.Json;

namespace RecipesFinalProjectController.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IRecipesService _recipesService;
        private readonly IFavoritesService _favoritesService;
        private readonly ICategoryService _categoryService;
        private readonly IDifficultyService _difficultyService;

        public SearchModel(
            IRecipesService recipesService,
            IFavoritesService favoritesService,
            ICategoryService categoryService,
            IDifficultyService difficultyService)
        {
            _recipesService = recipesService;
            _favoritesService = favoritesService;
            _categoryService = categoryService;
            _difficultyService = difficultyService;
        }

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
            Categories = _categoryService.RetrieveAll();
            Difficulties = _difficultyService.RetrieveAll();

            Recipes = _recipesService.Search(
                Title,
                CategoryId,
                DifficultyId,
                MaxTime);

            if (IsLoggedIn)
            {
                int userId =
                    HttpContext.Session.GetInt32("LoggedInUserId").Value;

                var favorites =
                    _favoritesService.GetUserFavorites(userId);

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

            if (_favoritesService.IsFavorite(userId.Value, recipeId))
                _favoritesService.RemoveFavorite(userId.Value, recipeId);
            else
                _favoritesService.AddFavorites(userId.Value, recipeId);

            return RedirectToPage();
        }
    }
}

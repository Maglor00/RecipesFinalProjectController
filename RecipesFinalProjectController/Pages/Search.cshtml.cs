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

        public SearchModel(IRecipesService recipesService, 
                           IFavoritesService favoritesService)
        {
            _recipesService = recipesService;
            _favoritesService = favoritesService;
        }

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? DifficultyId { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? MaxTime { get; set; }

        public List<Recipes> Recipes { get; set; }
        public List<Category> Categories { get; set; }
        public List<Difficulty> Difficulties { get; set; }
        public bool IsLoggedIn { get; set; }
        
        public void OnGet()
        {
            Recipes = _recipesService.Search(Title, CategoryId, DifficultyId, MaxTime);

            IsLoggedIn = HttpContext.Session.GetString("LoggedInUser") != null;

            Categories = new List<Category>();
            Difficulties = new List<Difficulty>();
        }

        public IActionResult OnPost(int recipeId)
        {
            var userJson = HttpContext.Session.GetString("LoggedInUser");

            if (userJson == null)
                return RedirectToPage("/Login");

            var user = JsonSerializer.Deserialize<Users>(userJson);

            _favoritesService.AddFavorites(user.Id, recipeId);

            return RedirectToPage();
        }
    }
}

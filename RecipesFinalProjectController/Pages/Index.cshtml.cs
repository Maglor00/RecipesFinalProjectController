using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;

namespace RecipesFinalProjectController.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Recipes> TopRecipes { get; set; } = new();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            TopRecipes = RecipesService.RetrieveTopRecipes();
        }
    }
}

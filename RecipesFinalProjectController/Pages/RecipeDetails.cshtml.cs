using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;

namespace RecipesFinalProjectController.Pages
{
    public class RecipeDetailsModel : PageModel
    {
        public Recipes Recipe { get; set; }
        public List<IngredientLine> Ingredients { get; set; }
        public List<Comments> Comments { get; set; } = new();
        public double AverageRating { get; set; }

        public IActionResult OnGet(int id)
        {
            Recipe = RecipesService.Retrieve(id);
            Ingredients = IngredientLineService.RetrieveByRecipeId(id);
            Comments = CommentsService.RetrieveRecipeComments(id);
            AverageRating = RatingService.RetrieveAverageRating(id);

            return Page();
        }
    }
}

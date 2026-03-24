using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;

namespace RecipesFinalProjectController.Pages
{
    public class RecipeDetailsModel : PageModel
    {
        public Recipes Recipe { get; set; } = new();
        public List<IngredientLine> Ingredients { get; set; } = new();
        public List<Comments> Comments { get; set; } = new();
        public double AverageRating { get; set; }
        public bool IsFavorite { get; set; }

        [BindProperty]
        public int Score { get; set; }

        [BindProperty]
        public string CommentText { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            LoadPageData(id);
            return Page();
        }

        public IActionResult OnPostToggleFavorite(int recipeId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            FavoritesService.ToggleFavorite(userId, recipeId);

            return RedirectToPage(new { id = recipeId });
        }

        public IActionResult OnPostRate(int recipeId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            RatingService.AddRating(userId, recipeId, Score);

            return RedirectToPage(new { id = recipeId });
        }

        public IActionResult OnPostComment(int recipeId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            CommentsService.AddComment(userId, recipeId, CommentText);

            return RedirectToPage(new { id = recipeId });
        }

        private void LoadPageData(int recipeId)
        {
            Recipe = RecipesService.Retrieve(recipeId);
            Ingredients = IngredientLineService.RetrieveByRecipeId(recipeId);
            Comments = CommentsService.RetrieveRecipeComments(recipeId);
            AverageRating = RatingService.RetrieveAverageRating(recipeId);

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                IsFavorite = FavoritesService.IsFavorite(userId, recipeId);

                var existingRating= RatingService.RetrieveByUserAndRecipe(userId, recipeId);
                if (existingRating != null)
                {
                    Score = existingRating.Score;
                }
                else
                {
                    Score = 0;
                }
            }
            else
            {
                IsFavorite = false;
                Score = 0;
            }
        }
    }
}
    


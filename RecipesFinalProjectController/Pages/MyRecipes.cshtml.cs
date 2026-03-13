using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;

namespace RecipesFinalProjectController.Pages
{
    public class MyRecipesModel : PageModel
    {
        public List<Recipes> Recipes { get; set; } = new();

        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Recipes = RecipesService.RetrieveByUserId(userId);

            return Page();
        }

        public IActionResult OnPostDelete(int recipeId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Recipes recipe = RecipesService.RetrieveByIdForUser(recipeId, userId);
            RecipesService.Delete(recipe.Id);

            Message = "Recipe deleted successfully.";
            return RedirectToPage();
        }
    }
}

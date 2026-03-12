using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;

namespace RecipesFinalProjectController.Pages.Admin
{
    public class RecipeApprovalModel : PageModel
    {
        public List<Recipes> PendingRecipes { get; set; } = new();

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated || !IsAdmin())
                return RedirectToPage("/Login");

            PendingRecipes = RecipesService.RetrievePendingRecipes();
            return Page();
        }

        public IActionResult OnPostApprove(int recipeId)
        {
            if (!User.Identity.IsAuthenticated || !IsAdmin())
                return RedirectToPage("/Login");

            RecipesService.ApproveRecipe(recipeId);
            TempData["Message"] = "Recipe approved successfully.";

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int recipeId)
        {
            if (!User.Identity.IsAuthenticated || !IsAdmin())
                return RedirectToPage("Login");

            RecipesService.Delete(recipeId);
            TempData["Message"] = "Pending recipe deleted successfully.";

            return RedirectToPage();
        }

        // Helper method to check if current user is admin
        private bool IsAdmin()
        {
            return User.FindFirst("is_admin")?.Value == "True";
        }
    }
}

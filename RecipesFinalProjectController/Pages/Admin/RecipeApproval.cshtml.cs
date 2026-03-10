using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;

namespace RecipesFinalProjectController.Pages.Admin
{
    public class RecipeApprovalModel : PageModel
    {
        public List<Recipes> PendingRecipes { get; set; }

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

            return RedirectToPage();
        }

        // Helper method to check if current user is admin
        private bool IsAdmin()
        {
            // Assumes is_admin is stored as a claim
            var isAdminClaim = User.FindFirst("is_admin");
            return isAdminClaim != null && isAdminClaim.Value == "True";
        }
    }
}

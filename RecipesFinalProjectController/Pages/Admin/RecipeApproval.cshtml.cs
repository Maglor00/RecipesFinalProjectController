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
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            bool isAdmin = User.FindFirst("is_admin")?.Value == "True";
            if (!isAdmin)
                return RedirectToPage("/Index");

            PendingRecipes = RecipesService.RetrievePendingRecipes();
            return Page();
        }

        public IActionResult OnPostApprove(int recipeId)
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            bool isAdmin = User.FindFirst("is_admin")?.Value == "True";
            if (!isAdmin)
                return RedirectToPage("/Index");

            RecipesService.ApproveRecipe(recipeId);
            TempData["Message"] = "Recipe approved successfully.";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int recipeId)
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            bool isAdmin = User.FindFirst("is_admin")?.Value == "True";
            if (!isAdmin)
                return RedirectToPage("/Index");

            RecipesService.Delete(recipeId);
            TempData["Message"] = "Recipe deleted successfully.";
            return RedirectToPage();
        }
    }
}

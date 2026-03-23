using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;

namespace RecipesFinalProjectController.Pages.Admin
{
    public class RecipeApprovalModel : PageModel
    {
        public List<Recipes> PendingRecipes { get; set; } = new();
        public List<Category> PendingCategories { get; set; } = new();
        public List<Difficulty> PendingDifficulties { get; set; } = new();

        public IActionResult OnGet()
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            bool isAdmin = User.FindFirst("is_admin")?.Value == "True";
            if (!isAdmin)
                return RedirectToPage("/Index");

            LoadData();
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

        public IActionResult OnPostApproveCategory(int categoryId)
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            bool isAdmin = User.FindFirst("is_admin")?.Value == "True";
            if (!isAdmin)
                return RedirectToPage("/Index");

            CategoryService.Approve(categoryId);
            TempData["Message"] = "Category approved successfully.";
            return RedirectToPage();
        }

        public IActionResult OnPostApproveDifficulty(int difficultyId)
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            bool isAdmin = User.FindFirst("is_admin")?.Value == "True";
            if (!isAdmin)
                return RedirectToPage("/Index");

            DifficultyService.Approve(difficultyId);
            TempData["Message"] = "Difficulty approved successfully.";
            return RedirectToPage();
        }

        private void LoadData()
        {
            PendingRecipes = RecipesService.RetrievePendingRecipes();
            PendingCategories = CategoryService.RetrievePending();
            PendingDifficulties = DifficultyService.RetrievePending();
        }
    }
}

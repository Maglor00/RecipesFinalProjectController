using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using RecipesFinalProjectServices;
using System.Security.Claims;

namespace RecipesFinalProjectController.Pages
{
    public class CreateRecipeModel : PageModel
    {
        [BindProperty]
        public Recipes Recipe { get; set; }

        public List<Category> Categories { get; set; }
        public List<Difficulty> Difficulties { get; set; }
        
        
        public IActionResult OnGet()
        {
            // Only logged in users
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            LoadDropdowns();

            Recipe = new Recipes
            {
                Category = new Category(),
                Difficulty = new Difficulty()
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return Page();
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Recipe.User = new Users
            {
                Id = userId
            };

            RecipesService.Create(Recipe);

            return RedirectToPage("/Index");
        }

        private void LoadDropdowns()
        {
            Categories = CategoryRepo.RetrieveAll();
            Difficulties = DifficultyRepo.RetrieveAll();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;

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
            if (HttpContext.Session.GetString("LoggedInUserId") == null)
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
            if (HttpContext.Session.GetString("LoggedInUserId") == null)
            {
                return RedirectToPage("/Login");
            }

            int userId = int.Parse(HttpContext.Session.GetString("LoggedInUserId"));

            Recipe.User = new Users
            {
                Id = userId
            };

            RecipesRepo.Create(Recipe);

            return RedirectToPage("/Index");
        }

        private void LoadDropdowns()
        {
            Categories = CategoryRepo.RetrieveAll();
            Difficulties = DifficultyRepo.RetrieveAll();
        }
    }
}

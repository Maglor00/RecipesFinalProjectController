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

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        public int DifficultyId { get; set; }

        [BindProperty]
        public List<int> IngredientIds { get; set; }

        [BindProperty]
        public List<int>? Quantities { get; set; }

        [BindProperty]
        public List<string>? Measures { get; set; }

        public List<Category> Categories { get; set; }
        public List<Difficulty> Difficulties { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public string Message { get; set; }
        
        
        public IActionResult OnGet()
        {
            // Only logged in users
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            LoadDropdowns();

            Recipe = new Recipes();

            return Page();
        }

        public IActionResult OnPost()
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");


            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return Page();
            }

            int userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Recipe.User = new Users
            {
                Id = userId
            };

            Recipe.Category = new Category
            {
                Id = CategoryId
            };

            Recipe.Difficulty = new Difficulty
            {
                Id = DifficultyId
            };

            Recipes createdRecipe = RecipesService.Create(Recipe);

            // Insert ingredients
            for (int i = 0; i < IngredientIds.Count; i++)
            {
                IngredientLineService.Create(new IngredientLine
                {
                    Recipe = new Recipes { Id = createdRecipe.Id },
                    Ingredient = new Ingredients { Id = IngredientIds[i] },
                    Quantity = Quantities?[i] ?? 0,
                    Measure = Measures?[i] ?? ""
                });
            }

            Message = "Recipe submitted successfully. Waiting for admin approval.";

            return RedirectToPage("/Index");
        }

        private void LoadDropdowns()
        {
            Categories = CategoryRepo.RetrieveAll();
            Difficulties = DifficultyRepo.RetrieveAll();
            Ingredients = IngredientsRepo.RetrieveAll();
        }
    }
}

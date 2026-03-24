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
        private readonly IWebHostEnvironment _environment;

        public CreateRecipeModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public Recipes Recipe { get; set; } = new();

        [BindProperty]
        public int? CategoryId { get; set; }

        [BindProperty]
        public int? DifficultyId { get; set; }

        [BindProperty]
        public string? NewCategoryName { get; set; }

        [BindProperty]
        public string? NewDifficultyName { get;set; }

        [BindProperty]
        public List<int>? IngredientIds { get; set; }

        [BindProperty]
        public List<string>? NewIngredientNames { get; set; }

        [BindProperty]
        public List<decimal>? Quantities { get; set; }

        [BindProperty]
        public List<string>? Measures { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public List<Category> Categories { get; set; } = new();
        public List<Difficulty> Difficulties { get; set; } = new();
        public List<Ingredients> Ingredients { get; set; } = new();

        [TempData]
        public string Message { get; set; } = string.Empty;
        
        
        public IActionResult OnGet()
        {
            // Only logged in users
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            LoadDropdowns();

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                string imageUrl = await SaveImageAsync();

                RecipesService.CreateRecipeWithIngredients(
                    userId,
                    Recipe,
                    CategoryId,
                    DifficultyId,
                    NewCategoryName,
                    NewDifficultyName,
                    IngredientIds,
                    NewIngredientNames,
                    Quantities,
                    Measures,
                    imageUrl
                );

                Message = "Recipe submitted successfully. It is now pending admin approval.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        private async Task<string> SaveImageAsync()
        {
            if (ImageFile == null || ImageFile.Length <= 0)
                return string.Empty;

            string uploadsFolder = Path.Combine(_environment.WebRootPath, "img", "recipes");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string extension = Path.GetExtension(ImageFile.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            return $"/img/recipes/{fileName}";
        }

        private void LoadDropdowns()
        {
            Categories = CategoryRepo.RetrieveAll();
            Difficulties = DifficultyRepo.RetrieveAll();
            Ingredients = IngredientsRepo.RetrieveAll();
        }
    }
}

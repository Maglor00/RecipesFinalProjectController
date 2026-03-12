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
        public Recipes Recipe { get; set; }

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

        public List<Category> Categories { get; set; }
        public List<Difficulty> Difficulties { get; set; }
        public List<Ingredients> Ingredients { get; set; }

        [TempData]
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

        public async Task<IActionResult> OnPostAsync()
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            LoadDropdowns();

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Recipe.User = new Users { Id = userId };

                // Category: select existing or create new
                if (!string.IsNullOrWhiteSpace(NewCategoryName))
                {
                    Recipe.Category = CategoryService.RetrieveOrCreateByName(NewCategoryName);
                }
                else if (CategoryId.HasValue && CategoryId.Value > 0)
                {
                    Recipe.Category = new Category { Id = CategoryId.Value };
                }
                else
                {
                    throw new Exception("Please select or create a category.");
                }

                // Difficulty: select existing or create new
                if (!string.IsNullOrWhiteSpace(NewDifficultyName))
                {
                    Recipe.Difficulty = DifficultyService.RetrieveOrCreateByName(NewDifficultyName);
                }
                else if (DifficultyId.HasValue && DifficultyId.Value > 0)
                {
                    Recipe.Difficulty = new Difficulty { Id = DifficultyId.Value };
                }
                else
                {
                    throw new Exception("Please select or create a difficulty.");
                }

                // Save uploaded image inside the app
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "recipes");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string extension = Path.GetExtension(ImageFile.FileName);
                    string fileName = $"{Guid.NewGuid()}{extension}";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    Recipe.ImageUrl = $"/uploads/recipes/{fileName}";
                }

                // Create recipe (saved pending approval)
                Recipes createdRecipe = RecipesService.Create(Recipe);

                // Create ingredient lines and new ingredients if needed
                if (Quantities != null && Measures != null)
                {
                    int rowCount = Math.Max(
                        Quantities.Count,
                        Math.Max(
                            Measures?.Count ?? 0,
                            Math.Max(
                                IngredientIds?.Count ?? 0,
                                NewIngredientNames?.Count ?? 0
                            )
                        )
                    );

                    for (int i = 0; i < rowCount; i++)
                    {
                        Ingredients? ingredient = null;

                        string? newIngredientName =
                            (NewIngredientNames != null && i < NewIngredientNames.Count)
                                ? NewIngredientNames[i]
                                : null;

                        int? selectedIngredientId =
                            (IngredientIds != null && i < IngredientIds.Count)
                                ? IngredientIds[i]
                                : null;

                        decimal quantity =
                            (Quantities != null && i < Quantities.Count)
                                ? Quantities[i]
                                : 0;

                        string? measure =
                            (Measures != null && i < Measures.Count)
                                ? Measures[i]
                                : null;

                        if (!string.IsNullOrWhiteSpace(newIngredientName))
                        {
                            ingredient = IngredientsService.RetrieveOrCreateByName(newIngredientName);
                        }
                        else if (selectedIngredientId.HasValue && selectedIngredientId.Value > 0)
                        {
                            ingredient = new Ingredients { Id = selectedIngredientId.Value };
                        }

                        if (ingredient == null)
                            continue;

                        if (quantity <= 0)
                            continue;

                        if (string.IsNullOrWhiteSpace(measure))
                            continue;

                        IngredientLineService.Create(new IngredientLine
                        {
                            Recipe = new Recipes { Id = createdRecipe.Id },
                            Ingredient = ingredient,
                            Quantity = quantity,
                            Measure = measure
                        });
                    }
                }

                Message = "Recipe submitted successfully. It is now pending admin approval.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        private void LoadDropdowns()
        {
            Categories = CategoryRepo.RetrieveAll();
            Difficulties = DifficultyRepo.RetrieveAll();
            Ingredients = IngredientsRepo.RetrieveAll();
        }
    }
}

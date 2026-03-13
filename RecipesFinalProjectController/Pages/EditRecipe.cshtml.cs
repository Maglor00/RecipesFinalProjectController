using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesFinalProjectModels;
using RecipesFinalProjectServices;
using System.Security.Claims;

namespace RecipesFinalProjectController.Pages
{
    public class EditRecipeModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public EditRecipeModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public Recipes Recipe { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        public int DifficultyId { get; set; }

        [BindProperty]
        public List<int?>? IngredientIds { get; set; }

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
        public List<IngredientLine> ExistingIngredients { get; set; } = new();


        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            Recipe = RecipesService.RetrieveByIdForUser(id, userId);
            CategoryId = Recipe.Category?.Id ?? 0;
            DifficultyId = Recipe.Difficulty?.Id ?? 0;

            ExistingIngredients = IngredientLineService.RetrieveByRecipeId(id);

            LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            LoadDropdowns();

            try
            {
                Recipes existing = RecipesService.RetrieveByIdForUser(Recipe.Id, userId);

                existing.Title = Recipe.Title;
                existing.PreparationMethod = Recipe.PreparationMethod;
                existing.PreparationTime = Recipe.PreparationTime;
                existing.Category = new Category { Id = CategoryId };
                existing.Difficulty = new Difficulty { Id = DifficultyId };
                existing.User = new Users { Id = userId };

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

                    existing.ImageUrl = $"/uploads/recipes/{fileName}";
                }

                existing.IsApproved = false;

                RecipesService.Update(existing);
                IngredientLineService.DeleteByRecipeId(existing.Id);

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

                        if (ingredient == null || quantity <= 0 || string.IsNullOrWhiteSpace(measure))
                            continue;

                        IngredientLineService.Create(new IngredientLine
                        {
                            Recipe = new Recipes { Id = existing.Id },
                            Ingredient = ingredient,
                            Quantity = quantity,
                            Measure = measure
                        });
                    }
                }

                Message = "Recipe updated successfully. It's now pending admin approval.";
                return RedirectToPage("/MyRecipes");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ExistingIngredients = IngredientLineService.RetrieveByRecipeId(Recipe.Id);
                return Page();
            }
        }

        private void LoadDropdowns()
        {
            Categories = CategoryService.RetrieveAll();
            Difficulties = DifficultyService.RetrieveAll();
        }
    }
}

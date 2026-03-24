using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class RecipesService
    {
        
        public static Recipes Create(Recipes recipes)
        {
            if (recipes == null)
                throw new InvalidOperationException("Recipe is required.");

            if (string.IsNullOrWhiteSpace(recipes.Title))
                throw new InvalidOperationException("Please enter a valid title.");

            if (string.IsNullOrWhiteSpace(recipes.PreparationMethod))
                throw new InvalidOperationException("Please enter a valid preparation method.");

            if (recipes.PreparationTime <= 0)
                throw new InvalidOperationException("Please enter a valid preparation time.");

            if (recipes.Category == null || recipes.Category.Id <= 0)
                throw new InvalidOperationException("Please select a valid category.");

            if (recipes.Difficulty == null || recipes.Difficulty.Id <= 0)
                throw new InvalidOperationException("Please select a valid difficulty.");

            if (recipes.User == null || recipes.User.Id <= 0)
                throw new InvalidOperationException("Please login first.");

            recipes.IsApproved = false;
            
            return RecipesRepo.Create(recipes);
        }

        public static Recipes CreateRecipeWithIngredients(
           int userId,
           Recipes recipe,
           int? categoryId,
           int? difficultyId,
           string? newCategoryName,
           string? newDifficultyName,
           List<int>? ingredientIds,
           List<string>? newIngredientNames,
           List<decimal>? quantities,
           List<string>? measures,
           string? imageUrl)
        {
            if (recipe == null)
                throw new InvalidOperationException("Recipe is required.");

            recipe.User = new Users { Id = userId };
            recipe.ImageUrl = imageUrl ?? "";

            if (!string.IsNullOrWhiteSpace(newCategoryName))
            {
                recipe.Category = CategoryService.RetrieveOrCreateByName(newCategoryName);
            }
            else if (categoryId.HasValue && categoryId.Value > 0)
            {
                recipe.Category = new Category { Id = categoryId.Value };
            }
            else
            {
                throw new InvalidOperationException("Please select or create a category.");
            }

            if (!string.IsNullOrWhiteSpace(newDifficultyName))
            {
                recipe.Difficulty = DifficultyService.RetrieveOrCreateByName(newDifficultyName);
            }
            else if (difficultyId.HasValue && difficultyId.Value > 0)
            {
                recipe.Difficulty = new Difficulty { Id = difficultyId.Value };
            }
            else
            {
                throw new InvalidOperationException("Please select or create a difficulty.");
            }

            Recipes createdRecipe = Create(recipe);

            CreateIngredientLines(
                createdRecipe.Id,
                ingredientIds,
                newIngredientNames,
                quantities,
                measures);

            return createdRecipe;
        }

        public static Recipes UpdateRecipeWithIngredients(
            int userId,
            Recipes recipe,
            int categoryId,
            int difficultyId,
            List<int?>? ingredientIds,
            List<string>? newIngredientNames,
            List<decimal>? quantities,
            List<string>? measures,
            string? imageUrl)
        {
            Recipes existing = RetrieveByIdForUser(recipe.Id, userId);

            existing.Title = recipe.Title;
            existing.PreparationMethod = recipe.PreparationMethod;
            existing.PreparationTime = recipe.PreparationTime;
            existing.Category = new Category { Id = categoryId };
            existing.Difficulty = new Difficulty { Id = difficultyId };
            existing.User = new Users { Id = userId };

            if (!string.IsNullOrWhiteSpace(imageUrl))
                existing.ImageUrl = imageUrl;

            existing.IsApproved = false;

            Recipes updated = Update(existing);

            IngredientLineService.DeleteByRecipeId(updated.Id);

            List<int>? normalizedIngredientIds = ingredientIds?.Select(x => x ?? 0).ToList();

            CreateIngredientLines(
                updated.Id,
                normalizedIngredientIds,
                newIngredientNames,
                quantities,
                measures);

            return updated;
        }

        private static void CreateIngredientLines(
            int recipeId,
            List<int>? ingredientIds,
            List<string>? newIngredientNames,
            List<decimal>? quantities,
            List<string>? measures)
        {
            if (quantities == null || measures == null)
                return;

            int rowCount = Math.Max(
                quantities.Count,
                Math.Max(
                    measures.Count,
                    Math.Max(
                        ingredientIds?.Count ?? 0,
                        newIngredientNames?.Count ?? 0
                    )
                )
            );

            for (int i = 0; i < rowCount; i++)
            {
                Ingredients? ingredient = null;

                string? newIngredientName =
                    (newIngredientNames != null && i < newIngredientNames.Count)
                        ? newIngredientNames[i]
                        : null;

                int? selectedIngredientId =
                    (ingredientIds != null && i < ingredientIds.Count)
                        ? ingredientIds[i]
                        : null;

                decimal quantity =
                    (quantities != null && i < quantities.Count)
                        ? quantities[i]
                        : 0;

                string? measure =
                    (measures != null && i < measures.Count)
                        ? measures[i]
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
                    Recipe = new Recipes { Id = recipeId },
                    Ingredient = ingredient,
                    Quantity = quantity,
                    Measure = measure
                });
            }
        }

        public static Recipes Retrieve(int id)
        {
            return RecipesRepo.Retrieve(id);
        }

        public static List<Recipes> RetrieveAll()
        {
            return RecipesRepo.RetrieveAll();
        }

        public static List<Recipes> RetrieveByUserId(int userId)
        {
            return RecipesRepo.RetrieveByUserId(userId);
        }

        public static Recipes RetrieveByIdForUser(int recipeId, int userId)
        {
            Recipes recipe = RecipesRepo.Retrieve(recipeId);

            if (recipe.User == null || recipe.User.Id != userId)
            {
                throw new InvalidOperationException("You cannot edit this recipe");
            }
                
            return recipe;
        }

        public static List<Recipes> Search(string title, int? categoryId, int? difficultyId, double? maxTime)
        {
            return RecipesRepo.Search(title, categoryId, difficultyId, maxTime);
        }

        public static Recipes Update(Recipes recipes)
        {
            if (recipes == null || recipes.Id <= 0)
                throw new InvalidOperationException("Invalid recipe.");

            if (string.IsNullOrWhiteSpace(recipes.Title))
                throw new InvalidOperationException("Please enter a valid title.");

            if (string.IsNullOrWhiteSpace(recipes.PreparationMethod))
                throw new InvalidOperationException("Please enter a valid preparation method.");

            if (recipes.PreparationTime <= 0)
                throw new InvalidOperationException("Please enter a valid preparation time.");

            if (recipes.Category == null || recipes.Category.Id <= 0)
                throw new InvalidOperationException("Please select a valid category.");

            if (recipes.Difficulty == null || recipes.Difficulty.Id <= 0)
                throw new InvalidOperationException("Please select a valid difficulty.");

            if (recipes.User == null || recipes.User.Id <= 0)
                throw new InvalidOperationException("Invalid recipe owner.");

            return RecipesRepo.Update(recipes);
        }

        public static void Delete(int id)
        {
            RecipesRepo.Delete(id);
        }

        public static void DeleteForUser(int recipeId, int userId)
        {
            Recipes recipe = RetrieveByIdForUser(recipeId, userId);
            Delete(recipe.Id);
        }

        public static List<Recipes> RetrievePendingRecipes()
        {
            return RecipesRepo.RetrievePendingRecipes();
        }

        public static void ApproveRecipe(int id)
        {
            RecipesRepo.ApproveRecipe(id);
        }

        public static List<Recipes> RetrieveTopRecipes(int num)
        {
            //List<Recipes> recipes = RecipesRepo.RetrieveAll();
            //recipes.OrderBy(x => x.rating;
            //return recipes.Take(num).ToList();

            if (num <= 0)
                num = 5;

            return RecipesRepo.RetrieveTopRecipes(num);
        }
    }
}

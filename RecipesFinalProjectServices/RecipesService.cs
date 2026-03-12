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
                throw new InvalidOperationException("Recipe is required");

            if (string.IsNullOrWhiteSpace(recipes.Title))
                throw new InvalidOperationException("Please enter a valid title");

            if (string.IsNullOrWhiteSpace(recipes.PreparationMethod))
                throw new InvalidOperationException("Please enter a valid preparation method");

            if (recipes.Category == null || recipes.Category.Id <= 0)
                throw new InvalidOperationException("Please select a valid category");

            if (recipes.Difficulty == null || recipes.Difficulty.Id <= 0)
                throw new InvalidOperationException("Please select a valid difficulty");

            if (recipes.User == null || recipes.User.Id <= 0)
                throw new InvalidOperationException("Please login first");
            return RecipesRepo.Create(recipes);
        }

        public static Recipes Retrieve(int id)
        {
            return RecipesRepo.Retrieve(id);
        }

        public static List<Recipes> RetrieveAll()
        {
            return RecipesRepo.RetrieveAll();
        }

        public static List<Recipes> Search(string title, int? categoryId, int? difficultyId, double? maxTime)
        {
            return RecipesRepo.Search(title, categoryId, difficultyId, maxTime);
        }

        public static Recipes Update(Recipes recipes)
        {
            return RecipesRepo.Update(recipes);
        }

        public static void Delete(int id)
        {
            RecipesRepo.Delete(id);
        }

        public static List<Recipes> RetrievePendingRecipes()
        {
            return RecipesRepo.RetrievePendingRecipes();
        }

        public static void ApproveRecipe(int id)
        {
            RecipesRepo.ApproveRecipe(id);
        }

        public static List<Recipes> RetrieveTopRecipes()
        {
            return RecipesRepo.RetrieveTopRecipes();
        }
    }
}

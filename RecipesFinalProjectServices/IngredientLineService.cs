using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class IngredientLineService
    {

        public static IngredientLine Create(IngredientLine ingredientLine)
        {
            if (ingredientLine == null)
                throw new InvalidOperationException("Ingredient line is required");

            if (ingredientLine.Ingredient == null || ingredientLine.Ingredient.Id <= 0)
                throw new InvalidOperationException("Please select a valid ingredient");

            if (ingredientLine.Recipe == null || ingredientLine.Recipe.Id <= 0)
                throw new InvalidOperationException("Please select a valid recipe");

            if (ingredientLine.Quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than zero");

            return IngredientLineRepo.Create(ingredientLine);
        }

        public static IngredientLine Retrieve(int id)
        {
            return IngredientLineRepo.Retrieve(id);
        }

        public static List<IngredientLine> RetrieveAll()
        {
            return IngredientLineRepo.RetrieveAll();
        }

        public static List<IngredientLine> RetrieveByRecipeId(int recipeId)
        {
            return IngredientLineRepo.RetrieveByRecipeId(recipeId);
        }

        public static IngredientLine Update(IngredientLine ingredientLine)
        {
            return IngredientLineRepo.Update(ingredientLine);
        }

        public static void Delete(int id)
        {
            IngredientLineRepo.Delete(id);
        }

        public static void DeleteByRecipeId(int recipeId)
        {
            IngredientLineRepo.DeleteByRecipeId(recipeId);
        }
    }
}

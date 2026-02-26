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
            if(ingredientLine.Ingredient.Equals(null) || ingredientLine.Quantity.Equals(null)|| 
                ingredientLine.Measure.Equals(null))
            {
                throw new InvalidOperationException("Please insert valid parameters");
            }

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

        public static IngredientLine Update(IngredientLine ingredientLine)
        {
            return IngredientLineRepo.Update(ingredientLine);
        }

        public static void Delete(int id)
        {
            IngredientLineRepo.Delete(id);
        }
    }
}

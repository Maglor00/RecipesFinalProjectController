using RecipesFinalProjectModels;
using RecipesFinalProjectRepo.Interface;
using RecipesFinalProjectServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Implementations
{
    internal class IngredientLineService : IIngredientLineService
    {
        public readonly IIngredientLineRepo _ingredientLineRepo;

        public IngredientLineService (IIngredientLineRepo ingredientLineRepo)
        {
            _ingredientLineRepo = ingredientLineRepo;
        }

        public IngredientLine Create(IngredientLine ingredientLine)
        {
            if(ingredientLine.Ingredient.Equals(null) || ingredientLine.Quantity.Equals(null)|| 
                ingredientLine.Measure.Equals(null))
            {
                throw new InvalidOperationException("Please insert valid parameters");
            }

            return _ingredientLineRepo.Create(ingredientLine);
        }

        public IngredientLine Retrieve(int id)
        {
            return _ingredientLineRepo.Retrieve(id);
        }

        public List<IngredientLine> RetrieveAll()
        {
            return _ingredientLineRepo.RetrieveAll();
        }

        public IngredientLine Update(IngredientLine ingredientLine)
        {
            return _ingredientLineRepo.Update(ingredientLine);
        }

        public void Delete(int id)
        {
            _ingredientLineRepo.Delete(id);
        }
    }
}

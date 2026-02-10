using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface IIngredientLineRepo
    {
        IngredientLine Create(IngredientLine ingredientLine);
        IngredientLine Retrieve(int id);
        List<IngredientLine> RetrieveAll();
        IngredientLine Update(IngredientLine ingredientLine);
        void Delete(int id);
    }
}

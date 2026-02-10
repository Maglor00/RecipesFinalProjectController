using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Interface
{
    public interface IIngredientLineService
    {
        IngredientLine Create(IngredientLine ingredientLine);
        IngredientLine Retrieve(int id);
        List<IngredientLine> RetrieveAll();
        IngredientLine Update(IngredientLine ingredientLine);
        void Delete(int id);
    }
}

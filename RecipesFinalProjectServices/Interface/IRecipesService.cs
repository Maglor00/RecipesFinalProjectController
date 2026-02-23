using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Interface
{
    public interface IRecipesService
    {
        Recipes Create(Recipes recipes);
        Recipes Retrieve(int id);
        List<Recipes> RetrieveAll();
        List<Recipes> Search(string title, int? categoryId, int? difficultyId, double? maxTime);
        Recipes Update(Recipes recipes);
        void Delete(int id);
    }
}

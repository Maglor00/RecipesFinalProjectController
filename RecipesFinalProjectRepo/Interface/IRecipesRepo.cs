using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface IRecipesRepo
    {
        Recipes Create(Recipes recipes);
        Recipes Retrieve(int id);
        List<Recipes> RetrieveAll();
        Recipes Update(Recipes recipes);
        void Delete(int id);
    }
}

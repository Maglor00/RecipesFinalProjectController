using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface IIngredientsRepo
    {
        Ingredients Create(Ingredients ingredients);
        Ingredients Retrieve(int id);
        List<Ingredients> RetrieveAll();
        Ingredients Update(Ingredients ingredients);
        void Delete(int id);
    }
}

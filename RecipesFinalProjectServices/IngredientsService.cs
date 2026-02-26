using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class IngredientsService
    {

        public static Ingredients Create(Ingredients ingredients)
        {
            if (ingredients.Name.Equals(null))
            {
                throw new InvalidOperationException("Please enter a valid name");
            }
            return IngredientsRepo.Create(ingredients);
        }

        public static Ingredients Retrieve(int id)
        {
            return IngredientsRepo.Retrieve(id);
        }

        public static List<Ingredients> RetrieveAll()
        {
            return IngredientsRepo.RetrieveAll();
        }

        public static Ingredients Update(Ingredients ingredients)
        {
            return IngredientsRepo.Update(ingredients);
        }

        public static void Delete(int id)
        {
            IngredientsRepo.Delete(id);
        }
    }
}

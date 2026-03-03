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
            if (recipes.Title.Equals(null)) 
            {
                throw new InvalidOperationException("Please enter a valid Title");
            }
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
    }
}

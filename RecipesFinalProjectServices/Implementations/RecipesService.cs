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
    internal class RecipesService : IRecipesService
    {
        public readonly IRecipesRepo _recipeRepo;

        public RecipesService (IRecipesRepo recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        public Recipes Create(Recipes recipes)
        {
            if (recipes.Title.Equals(null)) 
            {
                throw new InvalidOperationException("Please enter a valid Title");
            }
            return _recipeRepo.Create(recipes);
        }

        public Recipes Retrieve(int id)
        {
            return _recipeRepo.Retrieve(id);
        }

        public List<Recipes> RetrieveAll()
        {
            return _recipeRepo.RetrieveAll();
        }

        public List<Recipes> Search(string title, int? categoryId, int? difficultyId, double? maxTime)
        {
            return _recipeRepo.Search(title, categoryId, difficultyId, maxTime);
        }

        public Recipes Update(Recipes recipes)
        {
            return _recipeRepo.Update(recipes);
        }

        public void Delete(int id)
        {
            _recipeRepo.Delete(id);
        }
    }
}

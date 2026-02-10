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
    internal class IngredientsService : IIngredientsService
    {
        public readonly IIngredientsRepo _ingredientsRepo;

        public IngredientsService (IIngredientsRepo ingredientsRepo)
        {
            _ingredientsRepo = ingredientsRepo; 
        }

        public Ingredients Create(Ingredients ingredients)
        {
            if (ingredients.Name.Equals(null))
            {
                throw new InvalidOperationException("Please enter a valid name");
            }
            return _ingredientsRepo.Create(ingredients);
        }

        public Ingredients Retrieve(int id)
        {
            return _ingredientsRepo.Retrieve(id);
        }

        public List<Ingredients> RetrieveAll()
        {
            return _ingredientsRepo.RetrieveAll();
        }

        public Ingredients Update(Ingredients ingredients)
        {
            return _ingredientsRepo.Update(ingredients);
        }

        public void Delete(int id)
        {
            _ingredientsRepo.Delete(id);
        }
    }
}

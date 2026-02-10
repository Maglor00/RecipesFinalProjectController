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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public Category Create(Category category)
        {
            if(category.Name.Equals(null))
            {
                throw new InvalidOperationException("The Category name can't be null");
            }

            return _categoryRepo.Create(category);
        }

        public Category Retrieve(int id)
        {
            return _categoryRepo.Retrieve(id);
        }

        public List<Category> RetrieveAll()
        {
            return _categoryRepo.RetrieveAll();
        }

        public Category Update(Category category)
        {
            return _categoryRepo.Update(category);
        }

        public void Delete(int id)
        {
            _categoryRepo.Delete(id);
        }
    }
}

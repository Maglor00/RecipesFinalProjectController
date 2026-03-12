using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class CategoryService
    {
        public static Category Create(Category category)
        {
            if(category.Name.Equals(null))
            {
                throw new InvalidOperationException("The Category name can't be null");
            }

            return CategoryRepo.Create(category);
        }

        public static Category Retrieve(int id)
        {
            return CategoryRepo.Retrieve(id);
        }

        public static List<Category> RetrieveAll()
        {
            return CategoryRepo.RetrieveAll();
        }

        public static Category RetrieveOrCreateByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("The Category name can't be null");

            return CategoryRepo.RetrieveOrCreateByName(name.Trim());
        }

        public static Category Update(Category category)
        {
            return CategoryRepo.Update(category);
        }

        public static void Delete(int id)
        {
            CategoryRepo.Delete(id);
        }
    }
}

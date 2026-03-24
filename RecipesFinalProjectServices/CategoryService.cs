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
            if (category == null || string.IsNullOrWhiteSpace(category.Name))
                throw new InvalidOperationException("The category name can't be empty.");

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
                throw new InvalidOperationException("The category name can't be empty");

            var existing = CategoryRepo.RetrieveByName(name);

            if (existing != null)
            {
                return existing;
            }

            return Create(new Category
            {
                Name = name,
                IsApproved = false
            });
        }

        public static List<Category> RetrievePending()
        {
            return CategoryRepo.RetrievePending();
        }

        public static Category Update(Category category)
        {
            if (category == null || category.Id <= 0)
                throw new InvalidOperationException("Invalid category.");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new InvalidOperationException("The category name can't be empty.");

            return CategoryRepo.Update(category);
        }

        public static void Approve(int id)
        {
            CategoryRepo.Approve(id);
        }

        public static void Delete(int id)
        {
            CategoryRepo.Delete(id);
        }
    }
}

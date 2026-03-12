using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public class DifficultyService
    {
        public static Difficulty Create(Difficulty difficulty)
        {
            if (difficulty.Name.Equals(null))
            {
                throw new InvalidOperationException("The Difficulty name can't be null");
            }

            return DifficultyRepo.Create(difficulty);
        }

        public static Difficulty Retrieve(int id)
        {
            return DifficultyRepo.Retrieve(id);
        }

        public static List<Difficulty> RetrieveAll()
        {
            return DifficultyRepo.RetrieveAll();
        }

        public static Difficulty RetrieveOrCreateByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("The Difficulty name can't be null");

            return DifficultyRepo.RetrieveOrCreateByName(name.Trim());
        }

        public static Difficulty Update(Difficulty difficulty)
        {
            return DifficultyRepo.Update(difficulty);
        }

        public static void Delete(int id)
        {
            DifficultyRepo.Delete(id);
        }
    }
}

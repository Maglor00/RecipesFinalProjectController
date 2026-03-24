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
            if (difficulty == null || string.IsNullOrWhiteSpace(difficulty.Name))
                throw new InvalidOperationException("The difficulty name can't be empty.");

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


            var existing = DifficultyRepo.RetrieveByName(name);
            if (existing != null)
            {
                return existing;
            }

            return Create(new Difficulty
            {
                Name = name,
                IsApproved = false,
            });
        }

        public static List<Difficulty> RetrievePending()
        {
            return DifficultyRepo.RetrievePending();
        }

        public static Difficulty Update(Difficulty difficulty)
        {
            if (difficulty == null || difficulty.Id <= 0)
                throw new InvalidOperationException("Invalid difficulty.");

            if (string.IsNullOrWhiteSpace(difficulty.Name))
                throw new InvalidOperationException("The difficulty name can't be empty.");

            return DifficultyRepo.Update(difficulty);
        }

        public static void Approve(int id)
        {
            DifficultyRepo.Approve(id);
        }

        public static void Delete(int id)
        {
            DifficultyRepo.Delete(id);
        }
    }
}

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
    public class DifficultyService : IDifficultyService
    {
        public readonly IDifficultyRepo _difficultyRepo;

        public DifficultyService(IDifficultyRepo difficultyRepo)
        {
            _difficultyRepo = difficultyRepo;
        }

        public Difficulty Create(Difficulty difficulty)
        {
            if (difficulty.Name.Equals(null))
            {
                throw new InvalidOperationException("The Difficulty name can't be null");
            }

            return _difficultyRepo.Create(difficulty);
        }

        public Difficulty Retrieve(int id)
        {
            return _difficultyRepo.Retrieve(id);
        }

        public List<Difficulty> RetrieveAll()
        {
            return _difficultyRepo.RetrieveAll();
        }

        public Difficulty Update(Difficulty difficulty)
        {
            return _difficultyRepo.Update(difficulty);
        }

        public void Delete(int id)
        {
            _difficultyRepo.Delete(id);
        }
    }
}

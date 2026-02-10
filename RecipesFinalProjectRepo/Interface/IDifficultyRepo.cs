using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface IDifficultyRepo
    {
        Difficulty Create(Difficulty difficulty);
        Difficulty Retrieve(int id);
        List<Difficulty> RetrieveAll();
        Difficulty Update(Difficulty difficulty);
        void Delete(int id);
    }
}

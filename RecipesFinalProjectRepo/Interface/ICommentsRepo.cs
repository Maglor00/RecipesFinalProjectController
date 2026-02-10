using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface ICommentsRepo
    {
        Comments Create(Comments comments);
        Comments Retrieve(int id);
        List<Comments> RetrieveAll();
        Comments Update(Comments comments);
        void Delete(int id);
    }
}

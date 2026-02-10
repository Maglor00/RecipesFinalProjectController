using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Interface
{
    public interface ICommentsService
    {
        Comments Create(Comments comments);
        Comments Retrieve(int id);
        List<Comments> RetrieveAll();
        Comments Update(Comments comments);
        void Delete(int id);
    }
}

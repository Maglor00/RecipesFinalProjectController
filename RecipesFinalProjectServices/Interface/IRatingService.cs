using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Interface
{
    public interface IRatingService
    {
        Rating Create(Rating rating);
        Rating Retrieve(int id);
        List<Rating> RetrieveAll();
        Rating Update(Rating rating);
        void Delete(int id);
    }
}

using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class RatingService 
    {

        public static Rating Create(Rating rating)
        {
            if (rating.Score.Equals(null))
            {
                throw new InvalidOperationException("Please enter a valid score");
            }
            return RatingRepo.Create(rating);
        }

        public static Rating Retrieve(int id)
        {
            return RatingRepo.Retrieve(id);
        }

        public static List<Rating> RetrieveAll()
        {
            return RatingRepo.RetrieveAll();
        }

        public static Rating Update(Rating rating)
        {
            return RatingRepo.Update(rating);
        }

        public static void Delete(int id)
        {
            RatingRepo.Delete(id);
        }
    }
}

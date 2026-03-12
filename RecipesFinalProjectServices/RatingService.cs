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
            if (rating == null)
                throw new InvalidOperationException("Rating is required");

            if (rating.Score < 1 || rating.Score > 5)
                throw new InvalidOperationException("Please enter a score between 1 and 5");

            if (rating.User == null || rating.User.Id <= 0)
                throw new InvalidOperationException("A valid user is required");

            if (rating.Recipe == null || rating.Recipe.Id <= 0)
                throw new InvalidOperationException("A valid recipe is required");

            return RatingRepo.Create(rating);
        }

        public static Rating AddRating(int userId, int recipeId, int score)
        {
            if (score < 1 || score > 5)
                throw new InvalidOperationException("Please enter a score between 1 and 5");

            return RatingRepo.AddOrUpdateRating(userId, recipeId, score);
        }

        public static Rating Retrieve(int id)
        {
            return RatingRepo.Retrieve(id);
        }

        public static List<Rating> RetrieveAll()
        {
            return RatingRepo.RetrieveAll();
        }

        public static double RetrieveAverageRating(int recipeId)
        {
            return RatingRepo.RetrieveAverageRating(recipeId);
        }

        public static Rating Update(Rating rating)
        {

            if (rating == null)
                throw new InvalidOperationException("Rating is required");

            if (rating.Id <= 0)
                throw new InvalidOperationException("A valid rating id is required");

            if (rating.Score < 1 || rating.Score > 5)
                throw new InvalidOperationException("Please enter a score between 1 and 5");

            return RatingRepo.Update(rating);
        }

        public static void Delete(int id)
        {
            RatingRepo.Delete(id);
        }
    }
}

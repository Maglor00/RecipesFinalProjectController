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

        public static Rating Create(Rating rating, int recipeId)
        {
            if (rating == null)
                throw new InvalidOperationException("Rating is required.");

            if (recipeId <= 0)
                throw new InvalidOperationException("A valid recipe is required.");

            if (rating.Score < 1 || rating.Score > 5)
                throw new InvalidOperationException("Please enter a score between 1 and 5.");

            if (rating.User == null || rating.User.Id <= 0)
                throw new InvalidOperationException("A valid user is required.");

            return RatingRepo.Create(rating, recipeId);
        }

        public static Rating AddRating(int userId, int recipeId, int score)
        {
            if (userId <= 0)
                throw new InvalidOperationException("Invalid user.");

            if (recipeId <= 0)
                throw new InvalidOperationException("Invalid recipe.");

            if (score < 1 || score > 5)
                throw new InvalidOperationException("Please enter a score between 1 and 5.");

            var existing = RatingRepo.RetrieveByUserAndRecipe(userId, recipeId);

            if (existing != null)
            {
                existing.Score = score;
                return RatingRepo.Update(existing, recipeId);
            }

            return Create(new Rating
            {
                Score = score,
                User = new Users 
                { 
                    Id = userId 
                }
            }, recipeId);
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
            if(recipeId <= 0)
            {
                throw new InvalidOperationException("Invalid recipe.");
            }
                
            return RatingRepo.RetrieveAverageRating(recipeId);
        }

        public static Rating? RetrieveByUserAndRecipe(int userId, int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
            {
                return null;
            }
                

            return RatingRepo.RetrieveByUserAndRecipe(userId, recipeId);
        }

        public static Rating Update(Rating rating, int recipeId)
        {

            if (rating == null || rating.Id <= 0)
                throw new InvalidOperationException("Invalid rating.");

            if (recipeId <= 0)
                throw new InvalidOperationException("A valid recipe is required.");

            if (rating.Score < 1 || rating.Score > 5)
                throw new InvalidOperationException("Please enter a score between 1 and 5.");

            if (rating.User == null || rating.User.Id <= 0)
                throw new InvalidOperationException("A valid user is required.");

            return RatingRepo.Update(rating, recipeId);
        }

        public static void Delete(int id)
        {
            RatingRepo.Delete(id);
        }
    }
}

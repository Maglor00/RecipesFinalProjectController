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
    internal class RatingService : IRatingService
    {
        public readonly IRatingRepo _ratingRepo;

        public RatingService (IRatingRepo ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        public Rating Create(Rating rating)
        {
            if (rating.Score.Equals(null))
            {
                throw new InvalidOperationException("Please enter a valid score");
            }
            return _ratingRepo.Create(rating);
        }

        public Rating Retrieve(int id)
        {
            return _ratingRepo.Retrieve(id);
        }

        public List<Rating> RetrieveAll()
        {
            return _ratingRepo.RetrieveAll();
        }

        public Rating Update(Rating rating)
        {
            return _ratingRepo.Update(rating);
        }

        public void Delete(int id)
        {
            _ratingRepo.Delete(id);
        }
    }
}

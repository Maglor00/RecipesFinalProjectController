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
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepo _favoritesRepo;

        public FavoritesService (IFavoritesRepo favoritesRepo)
        {
            _favoritesRepo = favoritesRepo;
        }

        public void AddFavorites(int userId, int recipeId)
        {
            if(!_favoritesRepo.IsFavorite(userId, recipeId))
            {
                _favoritesRepo.AddFavorite(userId, recipeId);
            }
        }

        public void RemoveFavorite(int userId, int recipeId)
        {
            _favoritesRepo.RemoveFavorite(userId, recipeId);
        }

        public List<Recipes> GetUserFavorites(int userId)
        {
            return _favoritesRepo.GetUserFavorites(userId);
        }

        public bool IsFavorite(int userId, int recipeId)
        {
            return _favoritesRepo.IsFavorite(userId, recipeId);
        }
    }
}

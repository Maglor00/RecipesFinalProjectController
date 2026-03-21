using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class FavoritesService
    {

        public static void AddFavorites(int userId, int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
                throw new InvalidOperationException("Invalid favorite request.");

            if (FavoritesRepo.IsFavorite(userId, recipeId))
                return;

            FavoritesRepo.AddFavorite(userId, recipeId);
        }

        public static void RemoveFavorite(int userId, int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
                throw new InvalidOperationException("Invalid favorite request.");

            FavoritesRepo.RemoveFavorite(userId, recipeId);
        }

        public static void ToggleFavorite(int userId, int recipeId)
        {
            if(IsFavorite(userId, recipeId))
            {
                RemoveFavorite(userId, recipeId);
            }
            else
            {
                AddFavorites(userId, recipeId);
            }
        }

        public static List<Recipes> GetUserFavorites(int userId)
        {
            return FavoritesRepo.GetUserFavorites(userId);
        }

        public static bool IsFavorite(int userId, int recipeId)
        {
            return FavoritesRepo.IsFavorite(userId, recipeId);
        }
    }
}

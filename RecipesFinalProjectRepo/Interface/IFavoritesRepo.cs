using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface IFavoritesRepo
    {
        void AddFavorite(int userId, int recipeId);
        void RemoveFavorite(int userId, int recipeId);
        List<Recipes> GetUserFavorites(int userId);
        bool IsFavorite(int userId, int recipeId);
    }
}

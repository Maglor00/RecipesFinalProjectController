using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Interface
{
    public interface IFavoritesService
    {
        void AddFavorites(int userId, int recipeId);
        void RemoveFavorite(int userId, int recipeId);
        List<Recipes> GetUserFavorites(int userId);
        bool IsFavorite(int userId, int recipeId);
    }
}

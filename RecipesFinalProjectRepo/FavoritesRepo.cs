using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class FavoritesRepo
    {
        public static void AddFavorite(int userId, int recipeId)
        {
            string sql = $"INSERT INTO Favorites (user_id, recipe_id) " +
                $"VALUES ({userId}, {recipeId})";

            SQL.ExecuteNonQuery(sql);
        }

        public static void RemoveFavorite(int userId, int recipeId)
        {
            string sql = $"DELETE FROM Favorites WHERE user_id = {userId} AND recipe_id = {recipeId}";

            SQL.ExecuteNonQuery(sql);
        }

        public static List<Recipes> GetUserFavorites(int userId)
        {
            string sql = $"SELECT Recipes.*, " +
                $"Category.Name AS category_name, " +
                $"Difficulty.name AS difficulty_name " +
                $"FROM Recipes " +
                $"JOIN Favorites ON Recipes.id = Favorites.recipe_id " +
                $"JOIN Category ON Recipes.category_id = Category.id " +
                $"JOIN Difficulty ON Recipes.difficulty_id = Difficulty.id " +
                $"WHERE Favorites.user_id = {userId}";

            SqlDataReader datareader = SQL.ExecuteQuery(sql);

            List<Recipes> recipes = new List<Recipes>();

            while (datareader.Read())
            {
                recipes.Add(Parse(datareader));
            }

            return recipes;
        }

        public static bool IsFavorite(int userId, int recipeId)
        {
            string sql = $"SELECT COUNT(*) FROM Favorites WHERE user_id = {userId} AND recipe_id = {recipeId}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                int count = Convert.ToInt32(dataReader[0]);
                return count > 0;
            }

            return false;
        }

        private static Recipes Parse(SqlDataReader dataReader)
        {
            return new Recipes
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Title = Convert.ToString(dataReader["title"]),
                PreparationMethod = Convert.ToString(dataReader["preparation_method"]),
                PreparationTime = Convert.ToInt32(dataReader["preparation_time"]),
                IsApproved = Convert.ToBoolean(dataReader["is_approved"]),
                Category = new Category
                {
                    Id = Convert.ToInt32(dataReader["category_id"]),
                    Name = Convert.ToString(dataReader["category_name"])
                },
                Difficulty = new Difficulty
                {
                    Id = Convert.ToInt32(dataReader["difficulty_id"]),
                    Name = Convert.ToString(dataReader["difficulty_name"])
                }
            };
        }
    }
}

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
                $"Categories.Name AS category_name, " +
                $"Difficulties.name AS difficulty_name " +
                $"FROM Recipes " +
                $"JOIN Favorites ON Recipes.id = Favorites.recipe_id " +
                $"JOIN Categories ON Recipes.category_id = Categories.id " +
                $"JOIN Difficulties ON Recipes.difficulty_id = Difficulties.id " +
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

            int count = SQL.ExecuteNonQuery(sql);

            return count > 0;
        }

        private static Recipes Parse(SqlDataReader dataReader)
        {
            return new Recipes
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Title = Convert.ToString(dataReader["title"]),
                PreparationMethod = Convert.ToString(dataReader["preparation_method"]),
                PreparationTime = Convert.ToDouble(dataReader["preparation_time"]),
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

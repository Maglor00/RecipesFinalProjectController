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
            if (IsFavorite(userId, recipeId))
                return;

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
            string sql = "SELECT Recipes.*, " +
                 "Category.name AS category_name, " +
                 "Difficulty.name AS difficulty_name, " +
                 "Users.username AS username " +
                 "FROM Recipes " +
                 "JOIN Favorites ON Recipes.id = Favorites.recipe_id " +
                 "JOIN Category ON Recipes.category_id = Category.id " +
                 "JOIN Difficulty ON Recipes.difficulty_id = Difficulty.id " +
                 "JOIN Users ON Recipes.user_id = Users.id " +
                 $"WHERE Favorites.user_id = {userId} " +
                 "ORDER BY Recipes.id DESC";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            List<Recipes> recipes = new List<Recipes>();

            while (dataReader.Read())
            {
                recipes.Add(Parse(dataReader));
            }

            return recipes;
        }

        public static bool IsFavorite(int userId, int recipeId)
        {
            string sql = $"SELECT * FROM Favorites WHERE user_id = {userId} AND recipe_id = {recipeId}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            return dataReader.HasRows;
        }

        private static Recipes Parse(SqlDataReader dataReader)
        {
            return new Recipes
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Title = Convert.ToString(dataReader["title"]),
                PreparationMethod = Convert.ToString(dataReader["preparation_method"]),
                PreparationTime = Convert.ToInt32(dataReader["preparation_time"]),
                ImageUrl = dataReader["image_url"] == DBNull.Value ? "" : Convert.ToString(dataReader["image_url"]),
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

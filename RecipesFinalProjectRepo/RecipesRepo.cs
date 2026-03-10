using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class RecipesRepo
    {
        public static Recipes Create(Recipes recipes)
        {
            string sql = "INSERT INTO Recipes " +
                 "(title, preparation_method, preparation_time, category_id, difficulty_id, user_id, is_approved) " +
                 $"VALUES ('{recipes.Title}', " +
                 $"'{recipes.PreparationMethod}', " +
                 $"{recipes.PreparationTime}, " +
                 $"{recipes.Category.Id}, " +
                 $"{recipes.Difficulty.Id}, " +
                 $"{recipes.User.Id}, 0);";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static List<Recipes> RetrievePendingRecipes()
        {
            string sql = "SELECT Recipes.*, Category.name AS category_name, Difficulty.name AS difficulty_name, Users.username " +
                "FROM Recipes " +
                "JOIN Category ON Recipes.category_id = Category.id " +
                "JOIN Difficulty ON Recipes.difficulty_id = Difficulty.id " +
                "JOIN Users ON Recipes.user_id = Users.id " +
                "WHERE Recipes.is_approved = 0";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            List<Recipes> list = new();

            while (dataReader.Read())
            {
                list.Add(Parse(dataReader));
            }

            return list;
        }

        public static void ApproveRecipe (int id)
        {
            string sql = $"UPDATE Recipes SET is_approved = 1 WHERE id = {id}";

            SQL.ExecuteNonQuery(sql);
        }
        public static Recipes Retrieve(int id)
        {
            string sql = $"SELECT * FROM Recipes WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Recipe with ID: {id} not found");
        }

        public static List<Recipes> RetrieveAll()
        {
            string sql = $"SELECT * FROM Recipes;";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Recipes> recipes = new List<Recipes>();
            while (dataReader.Read())
            {
                recipes.Add(Parse(dataReader));
            }
            return recipes;
        }

        public static List<Recipes> Search(string title, int? categoryId, int? difficultyId, double? maxTime)
        {
            string sql = "SELECT Recipes.*, " +
             "Category.name AS category_name, " +
             "Difficulty.name AS difficulty_name, " +
             "Users.username AS username " +
             "FROM Recipes " +
             "JOIN Category ON Recipes.category_id = Category.id " +
             "JOIN Difficulty ON Recipes.difficulty_id = Difficulty.id " +
             "JOIN Users ON Recipes.user_id = Users.id " +
             "WHERE Recipes.is_approved = 1 ";

            if (!string.IsNullOrEmpty(title))
                sql += $"AND LOWER(Recipes.title) LIKE LOWER('%{title}%') ";

            if (categoryId.HasValue)
                sql += $"AND Recipes.category_id = {categoryId.Value} ";

            if (difficultyId.HasValue)
                sql += $"AND Recipes.difficulty_id = {difficultyId.Value} ";

            if (maxTime.HasValue)
                sql += $"AND Recipes.preparation_time <= {maxTime.Value} ";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Recipes> recipes = new List<Recipes>();

            while (dataReader.Read())
            {
                recipes.Add(Parse(dataReader));
            }

            return recipes;
        }

        public static Recipes Update(Recipes recipesToUpdate)
        {
            if (recipesToUpdate.Id <= 0) throw new Exception($"Recipe id {recipesToUpdate.Id} invalid");
            string sql = $"UPDATE Recipes SET " +
                $"title = '{recipesToUpdate.Title}', preparation_method = '{recipesToUpdate.PreparationMethod}', " +
                $"preparation_time = '{recipesToUpdate.PreparationTime}', category_id = '{recipesToUpdate.Category.Id}', " +
                $"difficulty_id = '{recipesToUpdate.Difficulty.Id}', user_id = '{recipesToUpdate.User.Id}', " +
                $"is_approved = '{recipesToUpdate.IsApproved}' WHERE id = {recipesToUpdate.Id};";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(recipesToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Recipes WHERE id = {id};";
            SQL.ExecuteNonQuery(sql);
        }

        private static Recipes Parse(SqlDataReader dataReader)
        {
            Recipes recipes = new Recipes();

            recipes.Id = Convert.ToInt32(dataReader["id"]);
            recipes.Title = Convert.ToString(dataReader["title"]);
            recipes.PreparationMethod = Convert.ToString(dataReader["preparation_method"]);
            recipes.PreparationTime = Convert.ToInt32(dataReader["preparation_time"]);

            recipes.Category = new Category
            {
                Id = Convert.ToInt32(dataReader["category_id"]),
                Name = HasColumn(dataReader, "category_name")
                    ? Convert.ToString(dataReader["category_name"])
                    : ""
            };

            recipes.Difficulty = new Difficulty
            {
                Id = Convert.ToInt32(dataReader["difficulty_id"]),
                Name = HasColumn(dataReader, "difficulty_name")
                    ? Convert.ToString(dataReader["difficulty_name"])
                    : ""
            };

            recipes.User = new Users
            {
                Id = Convert.ToInt32(dataReader["user_id"]),
                Username = HasColumn(dataReader, "username")
                    ? Convert.ToString(dataReader["username"])
                    : ""
            };

            recipes.ImageUrl = HasColumn(dataReader, "image_url")
                    ? Convert.ToString(dataReader["image_url"])
                    : "";

            recipes.IsApproved = Convert.ToBoolean(dataReader["is_approved"]);

            return recipes;
        }

        private static bool HasColumn(SqlDataReader dataReader, string columnName)
        {
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (dataReader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}

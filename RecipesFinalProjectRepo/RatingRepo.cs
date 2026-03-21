using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class RatingRepo
    {
        public static Rating Create(Rating rating, int recipeId)
        {
            string sql = $"INSERT INTO Rating (score, user_id, recipe_id)" +
                $"VALUES ({rating.Score}, {rating.User.Id}, {recipeId});";

            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Rating? RetrieveByUserAndRecipe(int userId, int recipeId)
        {
            string sql = $"SELECT Rating.*, Users.username AS username FROM Rating " +
                         $"JOIN Users ON Rating.user_id = Users.id " +
                         $"WHERE Rating.user_id = {userId} AND Rating.recipe_id = {recipeId}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            return null;
        }

        public static Rating Retrieve(int id)
        {
            string sql = "SELECT Rating.*, Users.username AS username " +
                         "FROM Rating " +
                         "JOIN Users ON Rating.user_id = Users.id " +
                         $"WHERE Rating.id = {id}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            throw new Exception($"Rating with ID: {id} not found");
        }

        public static List<Rating> RetrieveAll()
        {
            string sql = "SELECT Rating.*, Users.username AS username " +
                         "FROM Rating " +
                         "JOIN Users ON Rating.user_id = Users.id";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Rating> ratings = new();

            while (dataReader.Read())
            {
                ratings.Add(Parse(dataReader));
            }

            return ratings;
        }

        public static double RetrieveAverageRating(int recipeId)
        {
            string sql = $"SELECT AVG(CAST(score AS FLOAT)) FROM Rating WHERE recipe_id = {recipeId}";
            SqlDataReader reader = SQL.ExecuteQuery(sql);

            if (reader.Read() && reader[0] != DBNull.Value)
                return Convert.ToDouble(reader[0]);

            return 0;
        }

        public static Rating Update(Rating ratingToUpdate, int recipeId)
        {

            string sql = "UPDATE Rating SET " +
                         $"score = {ratingToUpdate.Score}, " +
                         $"user_id = {ratingToUpdate.User.Id}, " +
                         $"recipe_id = {recipeId} " +
                     $"WHERE id = {ratingToUpdate.Id}";

            SQL.ExecuteNonQuery(sql);
            return Retrieve(ratingToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Rating WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static Rating Parse(SqlDataReader dataReader)
        {
            Rating rating = new Rating();

            rating.Id = Convert.ToInt32(dataReader["id"]);
            rating.Score = Convert.ToInt32(dataReader["score"]);

            rating.User = new Users
            {
                Id = Convert.ToInt32(dataReader["user_id"]),
                Username = HasColumn(dataReader, "username")
                    ? Convert.ToString(dataReader["username"])
                    : ""
            };

            return rating;
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}

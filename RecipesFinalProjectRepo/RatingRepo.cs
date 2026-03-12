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
        public static Rating Create(Rating rating)
        {
            string sql = $"INSERT INTO Rating (score, user_id, recipe_id)" +
                $"VALUES ('{rating.Score}', '{rating.User.Id}', '{rating.Recipe.Id}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Rating AddOrUpdateRating(int userId, int recipeId, int score)
        {
            string checkSql = $"SELECT * FROM Rating WHERE user_id = {userId} AND recipe_id = {recipeId}";

            SqlDataReader dataReader = SQL.ExecuteQuery(checkSql);

            if (dataReader.Read())
            {
                int existingId = Convert.ToInt32(dataReader["id"]);

                string updateSql = $"UPDATE Rating SET score = {score} WHERE id = {existingId}";

                SQL.ExecuteNonQuery(updateSql);
                return Retrieve(existingId);
            }

            return Create(new Rating
            {
                Score = score,
                User = new Users { Id = userId },
                Recipe = new Recipes { Id = recipeId }
            });
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
            List<Rating> ratings = new List<Rating>();

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

        public static Rating Update(Rating ratingToUpdate)
        {
            if (ratingToUpdate.Id <= 0) 
                throw new Exception($"Rating id {ratingToUpdate.Id} invalid");

            string sql = "UPDATE Rating SET " +
                $"score = {ratingToUpdate.Score}, " +
                $"user_id = {ratingToUpdate.User.Id}, " +
                $"recipe_id = {ratingToUpdate.Recipe.Id} " +
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

            rating.Recipe = new Recipes
            {
                Id = Convert.ToInt32(dataReader["recipe_id"])
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

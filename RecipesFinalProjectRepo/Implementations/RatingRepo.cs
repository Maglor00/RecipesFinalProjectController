using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Implementations
{
    internal class RatingRepo : IRatingRepo
    {
        private readonly string _tableName = "Rating";
        public Rating Create(Rating rating)
        {
            string sql = $"INSERT INTO {_tableName} (score, user_id, recipe_id)" +
                $"VALUES ('{rating.Score}', '{rating.User.Id}', '{rating.Recipe.Id}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public Rating Retrieve(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Rating with ID: {id} not found");
        }

        public List<Rating> RetrieveAll()
        {
            string sql = $"SELECT * FROM {_tableName}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Rating> ratings = new List<Rating>();
            while (dataReader.Read())
            {
                ratings.Add(Parse(dataReader));
            }
            return ratings;
        }

        public Rating Update(Rating ratingToUpdate)
        {
            if (ratingToUpdate.Id <= 0) throw new Exception($"User id {ratingToUpdate.Id} invalid");
            string sql = $"UPDATE {_tableName} SET score = '{ratingToUpdate.Score}', " +
                $"user_id = '{ratingToUpdate.User.Id}', " +
                $"recipe_id = '{ratingToUpdate.Recipe.Id}' " +
                $"WHERE id = {ratingToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(ratingToUpdate.Id);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private Rating Parse(SqlDataReader dataReader)
        {
            Rating rating = new Rating();
            rating.Id = Convert.ToInt32(dataReader["id"]);
            rating.Score = Convert.ToInt32(dataReader["score"]);
            rating.User.Id = Convert.ToInt32(dataReader["user_id"]);
            rating.Recipe.Id = Convert.ToInt32(dataReader["recipe_id"]);

            return rating;
        }
    }
}

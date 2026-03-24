using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public class DifficultyRepo
    {
        public static Difficulty Create(Difficulty difficulty)
        {
            string sql = $"INSERT INTO Difficulty (name) VALUES ('{difficulty.Name}', {(difficulty.IsApproved ? 1 : 0)});";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Difficulty Retrieve(int id)
        {
            string sql = $"SELECT * FROM Difficulty WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Difficulty with ID: {id} not found");
        }

        public static List<Difficulty> RetrieveAll()
        {
            string sql = $"SELECT * FROM Difficulty";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Difficulty> difficulties = new();

            while (dataReader.Read())
            {
                difficulties.Add(Parse(dataReader));
            }

            return difficulties;
        }

        public static Difficulty? RetrieveByName(string name)
        {
            string sql = $"SELECT * FROM Difficulty WHERE LOWER (name) = LOWER('{name}');";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if(dataReader.Read())
            {
                return Parse(dataReader);
            }

            return null;
        }

        public static List<Difficulty> RetrievePending()
        {
            string sql = $"SELECT * FROM Difficulty WHERE is_approved = 0";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Difficulty> difficulties = new();

            while (dataReader.Read())
            {
                difficulties.Add(Parse(dataReader));
            }

            return difficulties;
        }

        public static Difficulty Update(Difficulty difficultyToUpdate)
        {

            string sql = $"UPDATE Difficulty SET name = '{difficultyToUpdate.Name}' " +
                $"WHERE id = {difficultyToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(difficultyToUpdate.Id);
        }

        public static void Approve(int id)
        {
            string sql = $"UPDATE Difficulty SET is_approved = 1 WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }
        

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Difficulty WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static Difficulty Parse(SqlDataReader dataReader)
        {
            return new Difficulty
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
                IsApproved = Convert.ToBoolean(dataReader["is_approved"])
            }; 
        }
    }
}

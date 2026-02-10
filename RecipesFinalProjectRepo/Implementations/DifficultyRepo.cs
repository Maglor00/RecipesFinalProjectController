using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using RecipesFinalProjectRepo.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Implementations
{
    internal class DifficultyRepo : IDifficultyRepo
    {
        private readonly string _tableName = "Difficulty";
        public Difficulty Create(Difficulty difficulty)
        {
            string sql = $"INSERT INTO {_tableName} (name) VALUES ('{difficulty.Name}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public Difficulty Retrieve(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Difficulty with ID: {id} not found");
        }

        public List<Difficulty> RetrieveAll()
        {
            string sql = $"SELECT * FROM {_tableName}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Difficulty> difficulties = new List<Difficulty>();
            while (dataReader.Read())
            {
                difficulties.Add(Parse(dataReader));
            }
            return difficulties;
        }

        public Difficulty Update(Difficulty difficultyToUpdate)
        {
            if (difficultyToUpdate.Id <= 0) throw new Exception($"User id {difficultyToUpdate.Id} invalid");
            string sql = $"UPDATE {_tableName} SET name = '{difficultyToUpdate.Name}' " +
                $"WHERE id = {difficultyToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(difficultyToUpdate.Id);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private Difficulty Parse(SqlDataReader datareader)
        {
            Difficulty difficulty = new Difficulty();
            difficulty.Id = Convert.ToInt32(datareader["id"]);
            difficulty.Name = Convert.ToString(datareader["name"]);

            return difficulty;
        }
    }
}

using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class IngredientsRepo
    {
        public static Ingredients Create(Ingredients ingredients)
        {
            string sql = $"INSERT INTO Ingredients (name) VALUES ('{ingredients.Name}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Ingredients Retrieve(int id)
        {
            string sql = $"SELECT * FROM Ingredients WHERE id = {id}";
            SqlDataReader datareader = SQL.ExecuteQuery(sql);

            if (datareader.Read())
            {
                return Parse(datareader);
            }

            throw new Exception($"Ingredient with ID: {id} not found");
        }

        public static List<Ingredients> RetrieveAll()
        {
            string sql = $"SELECT * FROM Ingredients";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Ingredients> ingredients = new();

            while (dataReader.Read())
            {
                ingredients.Add(Parse(dataReader));
            }

            return ingredients;
        }

        public static Ingredients? RetrieveByName(string name)
        {
            string sql = $"SELECT * FROM Ingredients WHERE LOWER (name) = LOWER ('{name}');";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            return null;
        }

        public static Ingredients Update(Ingredients ingredientsToUpdate)
        {
            string sql = $"UPDATE Ingredients SET name = '{ingredientsToUpdate.Name}' " +
                $"WHERE id = {ingredientsToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(ingredientsToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Ingredients WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static Ingredients Parse(SqlDataReader dataReader)
        {
            return new Ingredients
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"])
            };
        }
    }
}

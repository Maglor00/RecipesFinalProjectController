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
    internal class IngredientsRepo : IIngredientsRepo
    {
        private readonly string _tableName = "Ingredients";
        public Ingredients Create(Ingredients ingredients)
        {
            string sql = $"INSERT INTO {_tableName} (name) VALUES ('{ingredients.Name}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public Ingredients Retrieve(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE id = {id}";
            SqlDataReader datareader = SQL.ExecuteQuery(sql);
            if (datareader.Read())
            {
                return Parse(datareader);
            }
            throw new Exception($"Ingredient with ID: {id} not found");
        }

        public List<Ingredients> RetrieveAll()
        {
            string sql = $"SELECT * FROM {_tableName}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Ingredients> ingredients = new List<Ingredients>();
            while (dataReader.Read())
            {
                ingredients.Add(Parse(dataReader));
            }
            return ingredients;
        }

        public Ingredients Update(Ingredients ingredientsToUpdate)
        {
            if (ingredientsToUpdate.Id <= 0) throw new Exception($"Ingredient id {ingredientsToUpdate.Id} invalid");
            string sql = $"UPDATE {_tableName} SET name = '{ingredientsToUpdate.Name}' " +
                $"WHERE id = {ingredientsToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(ingredientsToUpdate.Id);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private Ingredients Parse(SqlDataReader dataReader)
        {
            Ingredients ingredients = new Ingredients();
            ingredients.Id = Convert.ToInt32(dataReader["id"]);
            ingredients.Name = Convert.ToString(dataReader["name"]);

            return ingredients;
        }
    }
}

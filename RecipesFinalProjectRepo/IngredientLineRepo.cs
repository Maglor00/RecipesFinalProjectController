using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class IngredientLineRepo
    {
        
        public static IngredientLine Create(IngredientLine ingredientLine)
        {
            string sql = $"INSERT INTO IngredientLine (ingredient_id, quantity, measure, recipe_id) " +
                $"VALUES('{ingredientLine.Ingredient.Id}', '{ingredientLine.Quantity}', " +
                $"'{ingredientLine.Measure}', '{ingredientLine.Recipe.Id}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static IngredientLine Retrieve(int id)
        {
            string sql = $"SELECT * FROM IngredientLine WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Ingredient Line with ID: {id} not found");
        }

        public static List<IngredientLine> RetrieveAll()
        {
            string sql = $"SELECT * FROM IngredientLine";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<IngredientLine> ingredientLines = new List<IngredientLine>();
            while (dataReader.Read())
            {
                ingredientLines.Add(Parse(dataReader));
            }
            return ingredientLines;
        }

        public static IngredientLine Update(IngredientLine ingredientLineToUpdate)
        {
            if (ingredientLineToUpdate.Id <= 0) throw new Exception($"User id {ingredientLineToUpdate.Id} invalid");
            string sql = $"UPDATE IngredientLine SET " +
                $"ingredient_id = '{ingredientLineToUpdate.Ingredient.Id}', " +
                $"quantity = '{ingredientLineToUpdate.Quantity}', measure = '{ingredientLineToUpdate.Measure}', " +
                $"recipe_id = '{ingredientLineToUpdate.Recipe.Id}' WHERE id = {ingredientLineToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(ingredientLineToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM IngredientLine WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static IngredientLine Parse(SqlDataReader dataReader)
        {
            IngredientLine ingredientLine = new IngredientLine();
            ingredientLine.Id = Convert.ToInt32(dataReader["id"]);
            ingredientLine.Ingredient.Id = Convert.ToInt32(dataReader["ingredient_id"]);
            ingredientLine.Quantity = Convert.ToInt32(dataReader["quantity"]);
            ingredientLine.Measure = Convert.ToDecimal(dataReader["measure"]);
            ingredientLine.Recipe.Id = Convert.ToInt32(dataReader["recipe_id"]);

            return ingredientLine;
        }
    }
}

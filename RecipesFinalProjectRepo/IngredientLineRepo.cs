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
            string sql = $"INSERT INTO IngredientLine (quantity, measure, ingredient_id, recipe_id) " +
                         $"VALUES ({ingredientLine.Quantity}, '{ingredientLine.Measure}', " +
                         $"{ingredientLine.Ingredient.Id}, {ingredientLine.Recipe.Id});";

            int id = SQL.ExecuteNonQuery(sql);
            return ingredientLine;
        }

        public static IngredientLine Retrieve(int id)
        {
            string sql = "SELECT IngredientLine.*, Ingredients.name AS ingredient_name " +
                         "FROM IngredientLine " +
                         "JOIN Ingredients ON IngredientLine.ingredient_id = Ingredients.id " +
                         $"WHERE IngredientLine.id = {id}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            throw new Exception($"Ingredient Line with ID: {id} not found");
        }

        public static List<IngredientLine> RetrieveAll()
        {
            string sql = "SELECT IngredientLine.*, Ingredients.name AS ingredient_name " +
                         "FROM IngredientLine " +
                         "JOIN Ingredients ON IngredientLine.ingredient_id = Ingredients.id";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<IngredientLine> ingredientLines = new();

            while (dataReader.Read())
            {
                ingredientLines.Add(Parse(dataReader));
            }

            return ingredientLines;
        }

        public static List<IngredientLine> RetrieveByRecipeId(int recipeId)
        {
            string sql = "SELECT IngredientLine.*, Ingredients.name AS ingredient_name " +
                         "FROM IngredientLine " +
                         "JOIN Ingredients ON IngredientLine.ingredient_id = Ingredients.id " +
                         $"WHERE IngredientLine.recipe_id = {recipeId}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<IngredientLine> ingredientLines = new();

            while (dataReader.Read())
            {
                ingredientLines.Add(Parse(dataReader));
            }

            return ingredientLines;
        }

        public static IngredientLine Update(IngredientLine ingredientLineToUpdate)
        {
           
            string sql = $"UPDATE IngredientLine SET " +
                $"ingredient_id = '{ingredientLineToUpdate.Ingredient.Id}', " +
                $"quantity = '{ingredientLineToUpdate.Quantity}', " +
                $"measure = '{ingredientLineToUpdate.Measure}', " +
                $"recipe_id = '{ingredientLineToUpdate.Recipe.Id}' " +
                $"WHERE id = {ingredientLineToUpdate.Id}";

            SQL.ExecuteNonQuery(sql);

            return Retrieve(ingredientLineToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM IngredientLine WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        public static void DeleteByRecipeId(int recipeId)
        {
            string sql = $"DELETE FROM IngredientLine WHERE recipe_id = {recipeId}";
            SQL.ExecuteNonQuery(sql);
        }

        private static IngredientLine Parse(SqlDataReader dataReader)
        {
            IngredientLine ingredientLine = new IngredientLine();

            ingredientLine.Id = Convert.ToInt32(dataReader["id"]);
            ingredientLine.Quantity = Convert.ToDecimal(dataReader["quantity"]);
            ingredientLine.Measure = Convert.ToString(dataReader["measure"]);

            ingredientLine.Ingredient = new Ingredients
            {
                Id = Convert.ToInt32(dataReader["ingredient_id"]),
                Name = HasColumn(dataReader, "ingredient_name")
                    ? Convert.ToString(dataReader["ingredient_name"])
                    : ""
            };

            ingredientLine.Recipe = new Recipes
            {
                Id = Convert.ToInt32(dataReader["recipe_id"])
            };

            return ingredientLine;

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

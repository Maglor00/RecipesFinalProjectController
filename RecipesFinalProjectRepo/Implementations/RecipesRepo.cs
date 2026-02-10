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
    internal class RecipesRepo : IRecipesRepo
    {
        private readonly string _tableName = "Recipes";
        public Recipes Create(Recipes recipes)
        {
            string sql = $"INSERT INTO {_tableName} (title, preparation_method, preparation_time, category_id," +
                $"difficulty_id, user_id, is_approved) " +
                $"VALUES ('{recipes.Title}', '{recipes.PreparationMethod}', '{recipes.PreparationTime}', " +
                $"'{recipes.Category.Id}', '{recipes.Difficulty.Id}', '{recipes.User.Id}', '{recipes.IsApproved}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }   

        public Recipes Retrieve(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Recipe with ID: {id} not found");
        }

        public List<Recipes> RetrieveAll()
        {
            string sql = $"SELECT * FROM {_tableName};";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Recipes> recipes = new List<Recipes>();
            while (dataReader.Read())
            {
                recipes.Add(Parse(dataReader));
            }
            return recipes;
        }

        public Recipes Update(Recipes recipesToUpdate)
        {
            if (recipesToUpdate.Id <= 0) throw new Exception($"Recipe id {recipesToUpdate.Id} invalid");
            string sql = $"UPDATE {_tableName} SET " +
                $"title = '{recipesToUpdate.Title}', preparation_method = '{recipesToUpdate.PreparationMethod}', " +
                $"preparation_time = '{recipesToUpdate.PreparationTime}', category_id = '{recipesToUpdate.Category.Id}', " +
                $"difficulty_id = '{recipesToUpdate.Difficulty.Id}', user_id = '{recipesToUpdate.User.Id}, " +
                $"is_approved = '{recipesToUpdate.IsApproved}' WHERE id = {recipesToUpdate.Id};";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(recipesToUpdate.Id);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE id = {id};";
            SQL.ExecuteNonQuery(sql);
        }

        private Recipes Parse(SqlDataReader dataReader)
        {
            Recipes recipes = new Recipes();
            recipes.Id = Convert.ToInt32(dataReader["id"]);
            recipes.Title = Convert.ToString(dataReader["title"]);
            recipes.PreparationMethod = Convert.ToString(dataReader["preparation_method"]);
            recipes.PreparationTime = Convert.ToDouble(dataReader["preparation_time"]);
            recipes.Category.Id = Convert.ToInt32(dataReader["category_id"]);
            recipes.Difficulty.Id = Convert.ToInt32(dataReader["difficulty_id"]);
            recipes.User.Id = Convert.ToInt32(dataReader["user_id"]);
            recipes.IsApproved = Convert.ToBoolean(dataReader["is_approved"]);

            return recipes;
        }
    }
}

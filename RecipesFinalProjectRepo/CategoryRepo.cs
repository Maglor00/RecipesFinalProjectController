using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class CategoryRepo
    {
        public static Category Create(Category category)
        {
            string sql = $"INSERT INTO Category (name) VALUES ('{category.Name}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Category Retrieve(int id)
        {
            string sql = $"SELECT * FROM Category WHERE id = {id};";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Category with ID: {id} not found");
        }

        public static List<Category> RetrieveAll()
        {
            string sql = $"SELECT * FROM Category;";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Category> categories = new List<Category>();
            while (dataReader.Read())
            {
                categories.Add(Parse(dataReader));
            }
            return categories;
        }

        public static Category? RetrieveByName(string name)
        {
            string sql = $"SELECT * FROM Category WHERE LOWER(name) = LOWER ('{name}');";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            return null;
        }

        public static Category RetrieveOrCreateByName(string name)
        {
            var existing = RetrieveByName(name);
            if (existing != null)
            {
                return existing;
            }
                
            return Create(new Category
            {
                Name = name
            }); 
        }

        public static Category Update(Category categoryToUpdate)
        {
            if (categoryToUpdate.Id <= 0) throw new Exception($"Category id {categoryToUpdate.Id} invalid");
            string sql = $"UPDATE Category SET Name = '{categoryToUpdate.Name}' WHERE ID = {categoryToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(categoryToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Category WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static Category Parse(SqlDataReader dataReader)
        {
            Category category = new Category();
            category.Id = Convert.ToInt32(dataReader["id"]);
            category.Name = Convert.ToString(dataReader["name"]);

            return category;
        }
    }
}

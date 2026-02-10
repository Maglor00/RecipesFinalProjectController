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
    internal class CategoryRepo : ICategoryRepo
    {
        private readonly string _tablename = "Category";
        public Category Create(Category category)
        {
            string sql = $"INSERT INTO {_tablename} (name) VALUES ('{category.Name}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public Category Retrieve(int id)
        {
            string sql = $"SELECT * FROM {_tablename} WHERE id = {id};";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if(dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Category with ID: {id} not found");
        }

        public List<Category> RetrieveAll()
        {
            string sql = $"SELECT * FROM {_tablename};";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Category> categories = new List<Category>();
            while (dataReader.Read())
            {
                categories.Add(Parse(dataReader));
            }
            return categories;
        }

        public Category Update(Category categoryToUpdate)
        {
            if(categoryToUpdate.Id<=0) throw new Exception($"Category id {categoryToUpdate.Id} invalid");
            string sql = $"UPDATE {_tablename} SET Name = '{categoryToUpdate.Name}' WHERE ID = {categoryToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(categoryToUpdate.Id);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM {_tablename} WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private Category Parse(SqlDataReader dataReader)
        {
            Category category = new Category();
            category.Id = Convert.ToInt32(dataReader["id"]);
            category.Name = Convert.ToString(dataReader["name"]);

            return category;
        }
    }
}

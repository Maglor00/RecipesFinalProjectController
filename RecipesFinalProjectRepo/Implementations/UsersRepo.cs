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
    public class UsersRepo : IUsersRepo
    {
        private readonly string _tableName = "Users";
        public Users Create(Users user)
        {
            string sql = $"INSERT INTO {_tableName} (first_name, last_name, username, password, is_admin) " +
                $"VALUES ('{user.FirstName}', '{user.LastName}', '{user.Username}', " +
                $"'{user.Password}', '{user.IsAdmin}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public Users Login(string username, string password)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE username = '{username}' AND password = '{password}'";
            SqlDataReader datareader = SQL.ExecuteQuery(sql);
            if (datareader.Read())
            {
                return Parse(datareader);
            }
            return null;
        }

        public Users Retrieve(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"User with ID: {id} not found");
        }

        public List<Users> RetrieveAll()
        {
            string sql = $"SELECT * FROM {_tableName}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Users> users = new List<Users>();
            while (dataReader.Read())
            {
                users.Add(Parse(dataReader));
            }
            return users;
        }

        public Users Update(Users userToUpdate)
        {
            if (userToUpdate.Id <= 0) throw new Exception($"User id {userToUpdate.Id} invalid");
            string sql = $"UPDATE {_tableName} SET " +
                $"first_name = '{userToUpdate.FirstName}', last_name = '{userToUpdate.LastName}', " +
                $"username = '{userToUpdate.Username}', password = '{userToUpdate.Password}', " +
                $"is_admin = '{userToUpdate.IsAdmin}' WHERE id = {userToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(userToUpdate.Id);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        public Users Parse(SqlDataReader dataReader)
        {
            Users user = new Users();
            user.Id = Convert.ToInt32(dataReader["id"]);
            user.FirstName = Convert.ToString(dataReader["first_name"]);
            user.LastName = Convert.ToString(dataReader["last_name"]);
            user.Username = Convert.ToString(dataReader["username"]);
            user.Password = Convert.ToString(dataReader["password"]);
            user.IsAdmin = Convert.ToBoolean(dataReader["is_admin"]);

            return user;
        }
    }
}

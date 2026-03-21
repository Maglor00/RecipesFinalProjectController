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
    public static class UsersRepo 
    {
        public static Users Create(Users user)
        {
            string sql = $"INSERT INTO Users (first_name, last_name, username, password, is_admin) " +
                         $"VALUES ('{user.FirstName}', '{user.LastName}', '{user.Username}', " +
                         $"'{user.Password}', '{user.IsAdmin}');";

            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static bool UsernameExists(string username)
        {
            string sql = $"SELECT * FROM Users WHERE username = '{username}'";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            return dataReader.HasRows;
        }

        public static Users? RetrieveByUsername(string username)
        {
            string sql = $"SELECT * FROM Users WHERE username = '{username}'";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if(dataReader.Read())
            {
                return Parse(dataReader);
            }

            return null;
        }

        public static Users Retrieve(int id)
        {
            string sql = $"SELECT * FROM Users WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            throw new Exception($"User with ID: {id} not found");
        }

        public static List<Users> RetrieveAll()
        {
            string sql = $"SELECT * FROM Users";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Users> users = new();

            while (dataReader.Read())
            {
                users.Add(Parse(dataReader));
            }

            return users;
        }

        public static Users UpdateProfile(Users userToUpdate)
        {

            string sql = $"UPDATE Users SET " +
                         $"first_name = '{userToUpdate.FirstName}', " +
                         $"last_name = '{userToUpdate.LastName}', " +
                         $"username = '{userToUpdate.Username}' " +
                         $"WHERE id = {userToUpdate.Id}";

            SQL.ExecuteNonQuery(sql);
            return Retrieve(userToUpdate.Id);
        }

        public static void UpdatePasswordHash(int userId, string hashedPassword)
        {
            string sql = $"UPDATE Users SET password = '{hashedPassword}' WHERE id = {userId}";
            SQL.ExecuteNonQuery(sql);
        }

        public static Users Update(Users userToUpdate)
        {
           
            string sql = $"UPDATE Users SET " +
                         $"first_name = '{userToUpdate.FirstName}', " +
                         $"last_name = '{userToUpdate.LastName}', " +
                         $"username = '{userToUpdate.Username}', " +
                         $"password = '{userToUpdate.Password}', " +
                         $"is_admin = '{userToUpdate.IsAdmin}' " +
                         $"WHERE id = {userToUpdate.Id}";

            SQL.ExecuteNonQuery(sql);
            return Retrieve(userToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Users WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        public static Users Parse(SqlDataReader dataReader)
        {
            return new Users
            {
                Id = Convert.ToInt32(dataReader["id"]),
                FirstName = Convert.ToString(dataReader["first_name"]),
                LastName = Convert.ToString(dataReader["last_name"]),
                Username = Convert.ToString(dataReader["username"]),
                Password = Convert.ToString(dataReader["password"]),
                IsAdmin = Convert.ToBoolean(dataReader["is_admin"])
            };
        }
    }
}

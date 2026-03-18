using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class UsersRepo 
    {
        public static Users Create(Users user)
        {
            string checkSql = $"SELECT * FROM Users WHERE username = '{user.Username}'";
            SqlDataReader checkReader = SQL.ExecuteQuery(checkSql);

            if (checkReader.Read())
            {
                throw new Exception("Username already exists");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            string sql = $"INSERT INTO Users (first_name, last_name, username, password, is_admin) " +
                         $"VALUES ('{user.FirstName}', '{user.LastName}', '{user.Username}', " +
                         $"'{hashedPassword}', '{user.IsAdmin}');";

            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Users Login(string username, string password)
        {
            string sql = $"SELECT * FROM Users WHERE username = '{username}'";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (!dataReader.Read())
                return null;

            Users user = Parse(dataReader);

            try
            {
                bool valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (!valid)
                    return null;

                return user;
            }
            catch
            {
                // Stored password is not a BCrypt hash
                return null;
            }
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
            List<Users> users = new List<Users>();
            while (dataReader.Read())
            {
                users.Add(Parse(dataReader));
            }
            return users;
        }

        public static Users UpdateProfile(Users userToUpdate)
        {
            if (userToUpdate.Id <= 0)
                throw new Exception($"User id {userToUpdate.Id} invalid");

            string sql = $"UPDATE Users SET " +
                         $"first_name = '{userToUpdate.FirstName}', " +
                         $"last_name = '{userToUpdate.LastName}', " +
                         $"username = '{userToUpdate.Username}' " +
                         $"WHERE id = {userToUpdate.Id}";

            SQL.ExecuteNonQuery(sql);
            return Retrieve(userToUpdate.Id);
        }

        public static void ChangePassword(int userId, string currentPassword, string newPassword)
        {
            Users existingUser = Retrieve(userId);

            bool valid = BCrypt.Net.BCrypt.Verify(currentPassword, existingUser.Password);
            try
            {
                valid = BCrypt.Net.BCrypt.Verify(currentPassword, existingUser.Password);
            }
            catch
            {
                throw new Exception("Stored password is invalid. Please reset this account password.");
            }

            if (!valid)
                throw new Exception("Current password is incorrect.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            string sql = $"UPDATE Users SET password = '{hashedPassword}' WHERE id = {userId}";
            SQL.ExecuteNonQuery(sql);
        }

        public static Users Update(Users userToUpdate)
        {
            if (userToUpdate.Id <= 0) throw new Exception($"User id {userToUpdate.Id} invalid");
            string sql = $"UPDATE Users SET " +
                $"first_name = '{userToUpdate.FirstName}', last_name = '{userToUpdate.LastName}', " +
                $"username = '{userToUpdate.Username}', password = '{userToUpdate.Password}', " +
                $"is_admin = '{userToUpdate.IsAdmin}' WHERE id = {userToUpdate.Id}";
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

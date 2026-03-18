using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class UsersService
    {
        public static Users Create(Users user)
        {
            if (user == null)
                throw new InvalidOperationException("User is required");

            if (string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.LastName) ||
                string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                throw new InvalidOperationException("Please enter a valid name, username, and password");
            }
            return UsersRepo.Create(user);
        }

        public static Users Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("Username and Password are required");
            }
            
            return UsersRepo.Login(username, password);
        }

        public static Users Retrieve(int id)
        {
            return UsersRepo.Retrieve(id);
        }

        public static List<Users> RetrieveAll()
        {
            return UsersRepo.RetrieveAll();
        }

        public static Users UpdateProfile(Users user)
        {
            if (user == null || user.Id <= 0)
                throw new InvalidOperationException("Invalid user.");

            if (string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.LastName) ||
                string.IsNullOrWhiteSpace(user.Username))
            {
                throw new InvalidOperationException("First name, last name and username are required.");
            }

            return UsersRepo.UpdateProfile(user);
        }

        public static void ChangePassword(int userId, string currentPassword,  string newPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
                throw new InvalidOperationException("Current password and new password are required.");

            UsersRepo.ChangePassword(userId, currentPassword, newPassword);
        }

        public static Users Update(Users user)
        {
            return UsersRepo.Update(user);
        }

        public static void Delete(int id)
        {
            UsersRepo.Delete(id);
        }  
    }
}

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
                throw new InvalidOperationException("User is required.");

            if (string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.LastName) ||
                string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                throw new InvalidOperationException("Please enter a valid name, username, and password.");
            }

            if (UsersRepo.UsernameExists(user.Username))
                throw new InvalidOperationException("Username already exists.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return UsersRepo.Create(user);
        }

        public static Users Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException("Username and Password are required.");

            var user = UsersRepo.RetrieveByUsername(username);
            if (user == null)
                return null;

            try
            {
                bool valid = BCrypt.Net.BCrypt.Verify(password, user.Password);
                return valid ? user : null;
            }
            catch
            {
                return null;
            }
        }

        public static Users Retrieve(int id)
        {
            return UsersRepo.Retrieve(id);
        }

        public static List<Users> RetrieveAll()
        {
            return UsersRepo.RetrieveAll();
        }

        public static Users UpdateProfile(int userId, string firstName, string lastName, string username, string? avatarUrl)
        {
            if (userId <= 0)
                throw new InvalidOperationException("Invalid user.");

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidOperationException("First name, last name and username are required.");
            }

            Users current = UsersRepo.Retrieve(userId);

            Users? existing = UsersRepo.RetrieveByUsername(username.Trim());
            if (existing != null && existing.Id != userId)
                throw new InvalidOperationException("Username already exists.");

            return UsersRepo.UpdateProfile(new Users
            {
                Id = userId,
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Username = username.Trim(),
                AvatarUrl = string.IsNullOrWhiteSpace(avatarUrl) ? current.AvatarUrl : avatarUrl
            });
        }

        public static void ChangePassword(int userId, string currentPassword,  string newPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
                throw new InvalidOperationException("Current password and new password are required.");

            Users existingUser = UsersRepo.Retrieve(userId);

            bool valid;
            try
            {
                valid = BCrypt.Net.BCrypt.Verify(currentPassword, existingUser.Password);
            }
            catch
            {
                throw new InvalidOperationException("Stored password is invalid.");
            }

            if (!valid)
                throw new InvalidOperationException("Current password is incorrect.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            UsersRepo.UpdatePasswordHash(userId, hashedPassword);
        }

        public static Users Update(Users user)
        {
            if (user == null || user.Id <= 0)
                throw new InvalidOperationException("Invalid user.");

            return UsersRepo.Update(user);
        }

        public static void Delete(int id)
        {
            UsersRepo.Delete(id);
        }  
    }
}

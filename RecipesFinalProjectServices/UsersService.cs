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
            if(user.FirstName.Equals(null) || user.LastName.Equals(null) || user.Username.Equals(null))
            {
                throw new InvalidOperationException("Please enter a valid name and/or username");
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

using RecipesFinalProjectModels;
using RecipesFinalProjectRepo.Interface;
using RecipesFinalProjectServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Implementations
{
    public class UsersService : IUsersService
    {
        public readonly IUsersRepo _userRepo;

        public UsersService (IUsersRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public Users Create(Users user)
        {
            if(user.FirstName.Equals(null) || user.LastName.Equals(null) || user.Username.Equals(null))
            {
                throw new InvalidOperationException("Please enter a valid name and/or username");
            }
            return _userRepo.Create(user);
        }

        public Users Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("Username and Password are required");
            }
            
            return _userRepo.Login(username, password);
        }

        public Users Retrieve(int id)
        {
            return _userRepo.Retrieve(id);
        }

        public List<Users> RetrieveAll()
        {
            return _userRepo.RetrieveAll();
        }

        public Users Update(Users user)
        {
            return _userRepo.Update(user);
        }

        public void Delete(int id)
        {
            _userRepo.Delete(id);
        }  
    }
}

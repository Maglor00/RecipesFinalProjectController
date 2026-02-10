using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo.Interface
{
    public interface IUsersRepo
    {
        Users Create(Users user);
        Users Retrieve(int id);
        List<Users> RetrieveAll();
        Users Login(string username, string password);
        Users Update(Users user);
        void Delete(int id);
    }
}

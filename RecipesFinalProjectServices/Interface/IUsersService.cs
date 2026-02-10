using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Interface
{
    public interface IUsersService
    {
        Users Create(Users user);
        Users Login(string  username, string password);
        Users Retrieve(int id);
        List<Users> RetrieveAll();
        Users Update(Users user);
        void Delete(int id);
    }
}

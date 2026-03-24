using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string AvatarUrl { get; set; } = string.Empty;

        public string ToString()
        {
            return "\tId:\t" + Id + "\tFirst Name:\t" + FirstName + "\tLaste Name:\t" + LastName + 
                "\tUsername:\t" + Username + "\tPassword:\t" + Password;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}

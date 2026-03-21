using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Users User { get; set; }
        

        public string ToString()
        {
            return "\tId:\t" + Id + "\tScore:\t" + Score + "\tUser:\t" + User;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}

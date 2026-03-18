using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ToString()
        {
            return "\tId:\t" + Id + "\tName:\t" + Name;
        }

    }
}

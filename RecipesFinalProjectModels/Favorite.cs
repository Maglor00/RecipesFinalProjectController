using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class Favorite
    {
        public int Id { get; set; }
        public Users User { get; set; }
        public Recipes Recipe { get; set; }
    }
}

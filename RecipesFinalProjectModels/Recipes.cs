using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class Recipes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IngredientLine IngredientLine { get; set; }
        public string PreparationMethod { get; set; }
        public double PreparationTime { get; set; }
        public Category Category { get; set; }
        public Difficulty Difficulty { get; set; }
        public Users User { get; set; }
        public bool IsApproved { get; set; }

        public string ToString()
        {
            return "\tId:\t" + Id + "\tTitle:\t" + Title + "\tPreparation Method:\t" + PreparationMethod + 
                "\tPeparation Time:\t" + PreparationTime + "\tCategory:\t" + Category + "\tDifficulty:\t" + Difficulty + 
                "\tUser:\t" + User;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}

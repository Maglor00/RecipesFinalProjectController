using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class IngredientLine
    {
        public int Id { get; set; }
        public Ingredients Ingredient { get; set; }
        public int Quantity { get; set; }
        public decimal Measure { get; set; }
        public Recipes Recipe { get; set; }

        public string ToString()
        {
            return "\tId:\t" + Id + "\tIngredient:\t" + Ingredient + "\tQuantity:\t" + Quantity + 
                "\tMeasure:\t" + Measure + "\tRecipe:\t" + Recipe;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectModels
{
    public class Comments
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public Comments Reply { get; set; }
        public Users User { get; set; }
        public Recipes Recipe { get; set; }

        public string ToString()
        {
            return "\tId:\t" + Id + "\tComment:\t" + CommentText + "\tReply:\t" + Reply + 
                "\tUser:\t" + User + "\tRecipe:\t" + Recipe;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}

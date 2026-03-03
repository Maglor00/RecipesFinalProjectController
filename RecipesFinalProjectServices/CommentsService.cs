using RecipesFinalProjectModels;
using RecipesFinalProjectRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices
{
    public static class CommentsService
    {
        
        public static Comments Create(Comments comments)
        {
            if (comments.CommentText.Equals(null))
            {
                throw new InvalidOperationException("The Comment text can't be null");
            }

            return CommentsRepo.Create(comments);
        }

        public static Comments Retrieve(int id)
        {
            return CommentsRepo.Retrieve(id); 
        }

        public static List<Comments> RetrieveAll()
        {
            return CommentsRepo.RetrieveAll();
        }

        public static Comments Update(Comments comments)
        {
            return CommentsRepo.Update(comments);
        }

        public static void Delete(int id)
        {
            CommentsRepo.Delete(id);
        }
    }
}

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
            if (comments == null)
                throw new InvalidOperationException("Comment is required");

            if (string.IsNullOrWhiteSpace(comments.CommentText))
                throw new InvalidOperationException("The comment text can't be empty");

            if (comments.User == null || comments.User.Id <= 0)
                throw new InvalidOperationException("A valid user is required");

            if (comments.Recipe == null || comments.Recipe.Id <= 0)
                throw new InvalidOperationException("A valid recipe is required");

            return CommentsRepo.Create(comments);
        }

        public static Comments AddComment(int userId, int recipeId, string commentText)
        {
            if (userId <= 0)
                throw new InvalidOperationException("Invalid user.");

            if (recipeId <= 0)
                throw new InvalidOperationException("Invalid recipe.");

            if (string.IsNullOrWhiteSpace(commentText))
                throw new InvalidOperationException("The comment text can't be empty.");

            return CommentsRepo.Create(new Comments
            {
                CommentText = commentText.Trim(),
                User = new Users { Id = userId },
                Recipe = new Recipes { Id = recipeId },
                Reply = null
            });
        }

        public static Comments Retrieve(int id)
        {
            return CommentsRepo.Retrieve(id); 
        }

        public static List<Comments> RetrieveAll()
        {
            return CommentsRepo.RetrieveAll();
        }

        public static List<Comments> RetrieveRecipeComments(int recipeId)
        {
            return CommentsRepo.RetrieveRecipeComments(recipeId);
        }

        public static Comments Update(Comments comments)
        {
            if(comments == null || comments.Id <= 0)
                throw new InvalidOperationException("Invalid comment.");

            if (string.IsNullOrWhiteSpace(comments.CommentText))
                throw new InvalidOperationException("The comment text can't be empty.");

            if (comments.User == null || comments.User.Id <= 0)
                throw new InvalidOperationException("A valid user is required.");

            if (comments.Recipe == null || comments.Recipe.Id <= 0)
                throw new InvalidOperationException("A valid recipe is required.");

            return CommentsRepo.Update(comments);
        }

        public static void Delete(int id)
        {
            CommentsRepo.Delete(id);
        }
    }
}

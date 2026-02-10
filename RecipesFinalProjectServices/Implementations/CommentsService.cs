using RecipesFinalProjectModels;
using RecipesFinalProjectRepo.Interface;
using RecipesFinalProjectServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectServices.Implementations
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepo _commentsRepo;

        public CommentsService (ICommentsRepo commentsRepo)
        {
            _commentsRepo = commentsRepo;
        }
        public Comments Create(Comments comments)
        {
            if (comments.CommentText.Equals(null))
            {
                throw new InvalidOperationException("The Comment text can't be null");
            }

            return _commentsRepo.Create(comments);
        }

        public Comments Retrieve(int id)
        {
            return _commentsRepo.Retrieve(id); 
        }

        public List<Comments> RetrieveAll()
        {
            return _commentsRepo.RetrieveAll();
        }

        public Comments Update(Comments comments)
        {
            return _commentsRepo.Update(comments);
        }

        public void Delete(int id)
        {
            _commentsRepo.Delete(id);
        }
    }
}

using Microsoft.Data.SqlClient;
using RecipesFinalProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    public static class CommentsRepo
    {
        
        public static Comments Create(Comments comments)
        {
            string sql = $"INSERT INTO Comments (comment_text, user_id, recipe_id, reply_id) " +
                $"VALUES ('{comments.CommentText}', '{comments.User.Id}', '{comments.Recipe.Id}', " +
                $"'{comments.Reply.Id}');";
            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Comments Retrieve(int id)
        {
            string sql = $"SELECT * FROM Comments WHERE id = {id}";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            if (dataReader.Read())
            {
                return Parse(dataReader);
            }
            throw new Exception($"Comment with ID: {id} not found");
        }

        public static List<Comments> RetrieveAll()
        {
            string sql = $"SELECT * FROM Comments";
            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Comments> comments = new List<Comments>();
            while (dataReader.Read())
            {
                comments.Add(Parse(dataReader));
            }
            return comments;
        }

        public static Comments Update(Comments commentsToUpdate)
        {
            if (commentsToUpdate.Id <= 0) throw new Exception($"User id {commentsToUpdate.Id} invalid");
            string sql = $"UPDATE Comments SET comment_text = '{commentsToUpdate.CommentText}', " +
                $"user_id = '{commentsToUpdate.User.Id}', recipe_id = '{commentsToUpdate.Recipe.Id}'" +
                $"reply_id = '{commentsToUpdate.Reply.Id}' WHERE id = {commentsToUpdate.Id}";
            SQL.ExecuteNonQuery(sql);
            return Retrieve(commentsToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Comments WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static Comments Parse(SqlDataReader datareader)
        {
            Comments comments = new Comments();
            comments.Id = Convert.ToInt32(datareader["id"]);
            comments.CommentText = Convert.ToString(datareader["comment_text"]);
            comments.User.Id = Convert.ToInt32(datareader["user_id"]);
            comments.Recipe.Id = Convert.ToInt32(datareader["recipe_id"]);
            comments.Reply.Id = Convert.ToInt32(datareader["reply_id"]);

            return comments;
        }
    }
}

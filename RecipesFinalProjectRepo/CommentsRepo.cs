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
            string replyValue = comments.Reply == null ? "NULL" : comments.Reply.Id.ToString();

            string sql = $"INSERT INTO Comments (comment_text, user_id, recipe_id, reply_id) " +
                $"VALUES ('{comments.CommentText}', '{comments.User.Id}', '{comments.Recipe.Id}', " +
                $"'{comments.Reply.Id}', {replyValue});";

            int id = SQL.ExecuteNonQuery(sql);
            return Retrieve(id);
        }

        public static Comments Retrieve(int id)
        {
            string sql =
                "SELECT Comments.*, Users.username AS username " +
                "FROM Comments " +
                "JOIN Users ON Comments.user_id = Users.id " +
                $"WHERE Comments.id = {id}";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);

            if (dataReader.Read())
            {
                return Parse(dataReader);
            }

            throw new Exception($"Comment with ID: {id} not found");
        }

        public static List<Comments> RetrieveAll()
        {
            string sql =
                "SELECT Comments.*, Users.username AS username " +
                "FROM Comments " +
                "JOIN Users ON Comments.user_id = Users.id";

            SqlDataReader dataReader = SQL.ExecuteQuery(sql);
            List<Comments> comments = new List<Comments>();

            while (dataReader.Read())
            {
                comments.Add(Parse(dataReader));
            }

            return comments;
        }

        public static List<Comments> RetrieveRecipeComments(int recipeId)
        {
            string sql =
                "SELECT Comments.*, Users.username AS username " +
                "FROM Comments " +
                "JOIN Users ON Comments.user_id = Users.id " +
                $"WHERE Comments.recipe_id = {recipeId} " +
                "ORDER BY Comments.id DESC";

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
            if (commentsToUpdate.Id <= 0) 
                throw new Exception($"Comment id {commentsToUpdate.Id} invalid");

            string replyValue = commentsToUpdate.Reply == null ? "NULL" : commentsToUpdate.Reply.Id.ToString();

            string sql =
                "UPDATE Comments SET " +
                $"comment_text = '{commentsToUpdate.CommentText}', " +
                $"user_id = {commentsToUpdate.User.Id}, " +
                $"recipe_id = {commentsToUpdate.Recipe.Id}, " +
                $"reply_id = {replyValue} " +
                $"WHERE id = {commentsToUpdate.Id}";

            SQL.ExecuteNonQuery(sql);
            return Retrieve(commentsToUpdate.Id);
        }

        public static void Delete(int id)
        {
            string sql = $"DELETE FROM Comments WHERE id = {id}";
            SQL.ExecuteNonQuery(sql);
        }

        private static Comments Parse(SqlDataReader dataReader)
        {
            Comments comments = new Comments();

            comments.Id = Convert.ToInt32(dataReader["id"]);
            comments.CommentText = Convert.ToString(dataReader["comment_text"]);

            comments.User = new Users
            {
                Id = Convert.ToInt32(dataReader["user_id"]),
                Username = HasColumn(dataReader, "username")
                    ? Convert.ToString(dataReader["username"])
                    : ""
            };

            comments.Recipe = new Recipes
            {
                Id = Convert.ToInt32(dataReader["recipe_id"])
            };

            comments.Reply = dataReader["reply_id"] == DBNull.Value
                ? null
                : new Comments
                {
                    Id = Convert.ToInt32(dataReader["reply_id"])
                };

            return comments;
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}

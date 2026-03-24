using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    internal class SQL
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=JD_TP_RC_Recipes;
                                        Trusted_Connection=True;TrustServerCertificate=True;";

        static SqlConnection conn = new SqlConnection(connectionString);


        public static int ExecuteNonQuery(string sql)
        {
            PrepareConnection();
            
            if (sql.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
            {
                sql = sql + " SELECT CAST(scope_identity() AS int);";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                return Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                return Convert.ToInt32(sqlCommand.ExecuteNonQuery());
            }
        }

        public static SqlDataReader ExecuteQuery(string sql)
        {
            PrepareConnection();

            Console.WriteLine(sql);
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static void PrepareConnection()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            conn.Open();
        }
    }
}

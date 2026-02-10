using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesFinalProjectRepo
{
    internal class SQL
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=JD_TP_RC_Recipes;
                                        Trusted_Connection=True;TrustedServerCertificate=False";

        static SqlConnection conn = new SqlConnection(connectionString);


        public static int ExecuteNonQuery(string sql)
        {
            PrepareConnection();
            if (sql.StartsWith("Insert"))
            {
                sql = sql + $"SELECT CAST(scope_identity() AS int);";
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
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            return sqlCommand.ExecuteReader();
        }

        private static void PrepareConnection()
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }
    }
}

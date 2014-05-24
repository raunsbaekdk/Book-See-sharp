using System;
using System.Data.SqlClient;
using System.Diagnostics;
using ModelSQL;

namespace WebAPI.Security {
    public class AuthorizationSystem : IAuthorizationSystem {
        private SqlConnection sqlConnection = Sql.GetInstance().GetConnection();
        public bool IsAdmin(String mobile) {
            SqlCommand sqlCommand = new SqlCommand("SELECT count(*) FROM Users WHERE Mobile="+mobile+";",sqlConnection);
            int i = -1;
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                if(reader.Read()) {
                    i = Convert.ToInt32(reader[0]);
                }
            } catch(SqlException e) {
                Debug.WriteLine("Exception in Authorization - " + e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return i > 0;
        }
    }
}
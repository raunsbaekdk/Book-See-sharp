using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WebAPI.Models {
    public class AuthorizationSystem : IAuthorizationSystem {
        private SqlConnection sqlConnection = Sql.GetInstance().GetConnection();
        private SqlCommand sqlCommand;
        public bool IsAdmin(String mobile) {
            sqlCommand = new SqlCommand("SELECT count(*) FROM Users WHERE Mobile="+mobile+";",sqlConnection);
            int i = -1;
            try {
                i = sqlCommand.ExecuteNonQuery();
            } catch(SqlException e) {
                Debug.WriteLine("Exception in Authorization - "+e.Message);
            }
            return i > 0;
        }
    }
}
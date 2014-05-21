using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data.SqlClient;
using ModelSQL;
using System.Data;
using System.Web.Security;

namespace Book_See_sharp {
    public partial class Default : System.Web.UI.Page
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private SqlParameter sqlParameter;

        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = Sql.GetInstance().GetConnection();


            if (Request.RequestType == "POST")
            {
                String username = Request.Form["username"], password = Request.Form["password"];

                if (username != null && password != null)
                {
                    // Validate user against database
                    Boolean validated = this.validateUser(username, password);
                    if(validated == true)
                    {
                        FormsAuthentication.RedirectFromLoginPage(username, true);
                    }
                }
            }
        }


        public Boolean validateUser(String username, String password)
        {
            sqlCommand = new SqlCommand("SELECT mobile, passwordId FROM Users LEFT JOIN Passwords.id = Users.PasswordId WHERE mobile = @mobile AND Passwords.password = @password", sqlConnection);

            // mobile
            sqlParameter = new SqlParameter("@mobile", SqlDbType.Int);
            sqlParameter.Value = username;
            sqlCommand.Parameters.Add(sqlParameter);

            // password
            sqlParameter = new SqlParameter("@password", SqlDbType.Int);
            sqlParameter.Value = password;
            sqlCommand.Parameters.Add(sqlParameter);

            int i = -1;
            try
            {
                i = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // Du er en spade
            }

            return i > 0;
        }
    }
}
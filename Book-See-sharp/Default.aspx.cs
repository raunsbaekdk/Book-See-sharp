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
                    Debug.WriteLine(validated);
                    if(validated == true)
                    {
                        FormsAuthentication.RedirectFromLoginPage(username, true);
                        // Redirect user
                        //Response.Redirect(Request.Form["ReturnUrl"]);
                    }
                }
            }
        }


        public Boolean validateUser(String username, String password)
        {
            sqlCommand = new SqlCommand("SELECT mobile, passwordId FROM Users LEFT JOIN Passwords ON Passwords.id = Users.PasswordId WHERE mobile = @mobile AND Passwords.password = @password", sqlConnection);

            // mobile
            sqlParameter = new SqlParameter("@mobile", SqlDbType.Int);
            sqlParameter.Value = username;
            sqlCommand.Parameters.Add(sqlParameter);

            // password
            sqlParameter = new SqlParameter("@password", SqlDbType.Char);
            sqlParameter.Value = password;
            sqlCommand.Parameters.Add(sqlParameter);


            Boolean userValidated = false;
            try
            {
                object objectCount = sqlCommand.ExecuteScalar();
                int countUsers = (int)objectCount;


                if (countUsers > 0)
                    userValidated = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            return userValidated;
        }
    }
}
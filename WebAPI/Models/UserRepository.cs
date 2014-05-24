using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Http;
using Models;
using ModelSQL;

namespace WebAPI.Models {
    public class UserRepository : IUserRepository {
        private SqlConnection sqlConnection;

        public UserRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
        }

        public IEnumerable<User> GetAllUsers() {
           SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users",sqlConnection);
            List<User> users = new List<User>();
            SqlDataReader reader = null;
            try {
               reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    User u = new User();
                    u.Mobile = Convert.ToInt32(reader[0]);
                    u.Password = Convert.ToInt32(reader[1]);
                    u.Email = Convert.ToString(reader[4]);
                    u.IsAdmin = Convert.ToBoolean(reader[2]);
                    u.Name = Convert.ToString(reader[3]);
                    users.Add(u);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return users;
        }

        public User Get(int username) {
            SqlCommand sqlCommand = new SqlCommand("Select * FROM Users WHERE mobile=" + username, sqlConnection);
            User u = null;
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    u = new User();
                    u.Mobile = Convert.ToInt32(reader[0]);
                    u.Password = Convert.ToInt32(reader[1]);
                    u.Email = Convert.ToString(reader[4]);
                    u.IsAdmin = Convert.ToBoolean(reader[2]);
                    u.Name = Convert.ToString(reader[3]);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return u;
        }
        // TODO: hashing of the password.
        // more of an example that transactions can be used.
        public User Post(UserApiClass user) {
            SqlTransaction transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            
            int passwordId = CreatePassword(user.Password,transaction);
            if(passwordId > 0) {

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Users values(@mobile,@password,@admin,@name,@email);", sqlConnection);
                sqlCommand.Transaction = transaction;

                // mobile
                SqlParameter sqlParameter = new SqlParameter("@mobile", SqlDbType.Int);
                sqlParameter.Value = user.Mobile;
                sqlCommand.Parameters.Add(sqlParameter);

                // password
                sqlParameter = new SqlParameter("@password", SqlDbType.Int);
                sqlParameter.Value = passwordId;
                sqlCommand.Parameters.Add(sqlParameter);

                // admin
                sqlParameter = new SqlParameter("@admin", SqlDbType.Bit);
                sqlParameter.Value = user.IsAdmin;
                sqlCommand.Parameters.Add(sqlParameter);

                // name
                sqlParameter = new SqlParameter("@name", SqlDbType.NVarChar);
                sqlParameter.Value = user.Name;
                sqlCommand.Parameters.Add(sqlParameter);

                // email
                sqlParameter = new SqlParameter("@email", SqlDbType.NVarChar);
                sqlParameter.Value = user.Email;
                sqlCommand.Parameters.Add(sqlParameter);

                int i = -1;
                try {
                    i = sqlCommand.ExecuteNonQuery();
                    transaction.Commit();
                } catch(Exception e) {
                    Debug.WriteLine(e.Message);
                    transaction.Rollback();
                }
                User u = Get(user.Mobile);
                return u;
            }
            transaction.Rollback();
            return null;
        }
        // Rollback logic
        // returns the ID of the password created.
        private int CreatePassword(String password, SqlTransaction tran) {
            SqlCommand sqlCommand =
                new SqlCommand("INSERT INTO Passwords VALUES(@password,GETDATE()); SELECT Scope_Identity()",
                    sqlConnection);
            sqlCommand.Transaction = tran;
            int id = -1;
            // password - TODO: Redo for hashing!!!!!
            SqlParameter sqlParameter = new SqlParameter("@password", SqlDbType.Char);
            sqlParameter.Value = password;
            sqlCommand.Parameters.Add(sqlParameter);
            try {
                id = Convert.ToInt32(sqlCommand.ExecuteScalar());
            } catch(SqlException e) {
                Debug.WriteLine(e.Message);
            }
            return id;
        }

        public bool Delete(int mobile) {
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Users WHERE mobile=" + mobile + ";", sqlConnection);
            int i = -1;
            try {
                i = sqlCommand.ExecuteNonQuery();
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return i > 0;
        }

        public bool Put(User user) {
            SqlCommand sqlCommand =
                new SqlCommand(
                    "UPDATE User SET mobile=@mobile,password=@password,admin=@admin,name=@name,email=@email WHERE mobile=" +
                    user.Mobile + ";", sqlConnection);

            // mobile
            SqlParameter sqlParameter = new SqlParameter("@mobile", SqlDbType.Int);
            sqlParameter.Value = user.Mobile;
            sqlCommand.Parameters.Add(sqlParameter);

            // password
            sqlParameter = new SqlParameter("@password", SqlDbType.Int);
            sqlParameter.Value = user.Password;
            sqlCommand.Parameters.Add(sqlParameter);

            // admin
            sqlParameter = new SqlParameter("@admin", SqlDbType.Bit);
            sqlParameter.Value = user.IsAdmin;
            sqlCommand.Parameters.Add(sqlParameter);

            // name
            sqlParameter = new SqlParameter("@name", SqlDbType.NVarChar);
            sqlParameter.Value = user.Name;
            sqlCommand.Parameters.Add(sqlParameter);

            // email
            sqlParameter = new SqlParameter("@email", SqlDbType.NVarChar);
            sqlParameter.Value = user.Mobile;
            sqlCommand.Parameters.Add(sqlParameter);

            int i = -1;
            try {
                i = sqlCommand.ExecuteNonQuery();
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return i > 0;
        }

        public IEnumerable<Reservation> GetUserReservations(int mobile) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reservations WHERE username="+mobile+";");
            List<Reservation> list = null;
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                list = new List<Reservation>();
                while(reader.Read()) {
                    Reservation r = new Reservation();
                    r.Id = Convert.ToInt32(reader[0]);
                    r.User = Get(mobile);
                    r.ToDate = Convert.ToDateTime(reader[4]);
                    r.FromDate = Convert.ToDateTime(reader[3]);
                    r.Bus = GetBus(Convert.ToString(reader[2]));
                    list.Add(r);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null) {
                    reader.Close();
                }
            }
            return list;
        }

        private Bus GetBus(string bus) {
           SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Busses WHERE regNo='"+bus+"';");
            Bus b = null;
            try {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    b = new Bus();
                    b.RegNo = Convert.ToString(reader[0]);
                    b.ComCenter = Convert.ToString(reader[1]);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message); 
            }
            return b;
        }
    }
}
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

namespace WebAPI.Models {
    public class UserRepository : IUserRepository {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private SqlParameter sqlParameter;

        public UserRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
        }

        public IEnumerable<User> GetAllUsers() {
            sqlCommand = new SqlCommand("SELECT * FROM Users",sqlConnection);
            List<User> users = new List<User>();
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
                reader.Close();
            }
            return users;
        }

        public User Get(int username) {
            sqlCommand = new SqlCommand("Select * FROM User WHERE mobile=" + username, sqlConnection);
            User u = null;
            try {
                SqlDataReader reader = sqlCommand.ExecuteReader();
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
                reader.Close();
            }
            return u;
        }

        public User Add(User user) {
            sqlCommand = new SqlCommand("INSERT INTO User values(@mobile,@password,@admin,@name,@email);", sqlConnection);

            // mobile
            sqlParameter = new SqlParameter("@mobile", SqlDbType.Int);
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
            return Get(user.Mobile);
        }

        public void DeleteUser(int mobile) {
            sqlCommand = new SqlCommand("DELETE FROM User WHERE mobile=" + mobile + ";", sqlConnection);
            try {
                sqlCommand.ExecuteNonQuery();
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
        }

        public bool PutUser(User user) {
            sqlCommand =
                new SqlCommand(
                    "UPDATE User SET mobile=@mobile,password=@password,admin=@admin,name=@name,email=@email WHERE mobile=" +
                    user.Mobile + ";", sqlConnection);

            // mobile
            sqlParameter = new SqlParameter("@mobile", SqlDbType.Int);
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
            sqlCommand = new SqlCommand("SELECT * FROM Reservations WHERE username="+mobile+";");
            List<Reservation> list = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    Reservation r = new Reservation();
                    r.Id = Convert.ToInt32(reader[0]);
                    r.User = Get(mobile);
                    r.ToDate = Convert.ToDateTime(reader[4]);
                    r.FromDate = Convert.ToDateTime(reader[3]);
                    r.Bus = GetBus(Convert.ToString(reader[2]));
                    
                }
            } catch(Exception e) {
                 Debug.WriteLine(e.Message);       
            }
            return list;
        }

        private Bus GetBus(string bus) {
            sqlCommand = new SqlCommand("SELECT * FROM Busses WHERE regNo='"+bus+"';");
            Bus b = null;
            try {
                SqlDataReader reader2 = sqlCommand.ExecuteReader();
                while(reader2.Read()) {
                    b = new Bus();
                    b.RegNo = Convert.ToString(reader2[0]);
                    b.ComCenter = Convert.ToString(reader[1]);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message); 
            }
            return b;
        }
    }
}
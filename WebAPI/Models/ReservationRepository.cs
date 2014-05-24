using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Models;
using ModelSQL;
using WebAPI.Security;

namespace WebAPI.Models {
    public class ReservationRepository : IReservationRespository {
        private SqlConnection sqlConnection;
        private readonly IAuthorizationSystem AuthorizationSystem;

        public ReservationRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
            AuthorizationSystem = new AuthorizationSystem();
        }

        public IEnumerable<Reservation> GetAllReservations() {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reservations", sqlConnection);
            List<Reservation> reservations = null;
            try {
                reservations = new List<Reservation>();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    Reservation r = new Reservation();
                    r.Id = Convert.ToInt32(reader[0]);
                    r.FromDate = Convert.ToDateTime(reader[3]);
                    r.ToDate = Convert.ToDateTime(reader[4]);
                    r.Bus = GetBus(Convert.ToString(reader[2]));
                    r.User = GetUser(Convert.ToInt32(reader[1]));
                    reservations.Add(r);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return reservations;
        }

        public IEnumerable<Reservation> GetBusReservation(String regNo, DateTime date) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reservations r WHERE bus='AB12345' AND CONVERT(char(10),r.fromDate,126)=@date", sqlConnection);
            List<Reservation> list = new List<Reservation>();

            // regno
            SqlParameter sqlParameter = new SqlParameter("@regNo", SqlDbType.NVarChar);
            sqlParameter.Value = regNo;
            sqlCommand.Parameters.Add(sqlParameter);

            // date
            sqlParameter = new SqlParameter("@date", SqlDbType.DateTime);
            sqlParameter.Value = date;
            sqlCommand.Parameters.Add(sqlParameter);
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    Reservation r = new Reservation();
                    r.Id = Convert.ToInt32(reader[0]);
                    r.FromDate = Convert.ToDateTime(reader[4]);
                    r.ToDate = Convert.ToDateTime(reader[3]);
                    r.User = GetUser(Convert.ToInt32(reader[1]));
                    r.Bus = GetBus(Convert.ToString(reader[2]));
                    list.Add(r);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return list;
        }

        public IEnumerable<Reservation> GetBusReservation(string regNo) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reservations r WHERE bus='AB12345';", sqlConnection);
            List<Reservation> list = new List<Reservation>();

            // regno
            SqlParameter sqlParameter = new SqlParameter("@regNo", SqlDbType.NVarChar);
            sqlParameter.Value = regNo;
            sqlCommand.Parameters.Add(sqlParameter);
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    Reservation r = new Reservation();
                    r.Id = Convert.ToInt32(reader[0]);
                    r.FromDate = Convert.ToDateTime(reader[4]);
                    r.ToDate = Convert.ToDateTime(reader[3]);
                    r.User = GetUser(Convert.ToInt32(reader[1]));
                    r.Bus = GetBus(Convert.ToString(reader[2]));
                    list.Add(r);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return list;
        }

        private Bus GetBus(String regNo) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Busses WHERE regNo='" + regNo + "'", sqlConnection);
            SqlDataReader reader = null;
            Bus b = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    b = new Bus();
                    b.RegNo = Convert.ToString(reader[0]);
                    b.ComCenter = Convert.ToString(reader[1]);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null) {
                    reader.Close();
                }
            }
            return b;
        }

        private User GetUser(int id) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Mobile=" + id, sqlConnection);
            SqlDataReader reader = null;
            User u = null;
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
                if(reader != null) {
                    reader.Close();
                }
            }
            return u;
        }

        public int DeleteReservation(int reservationId) {
            // check if it's the users own reservation
            SqlCommand sqlCommand = new SqlCommand("SELECT username FROM reservations WHERE id=" + reservationId + ";",sqlConnection);
            string username = HttpContext.Current.User.Identity.Name;
            bool ownReservation = false;
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                if(reader.Read()) {
                    ownReservation = Convert.ToInt32(reader[0]) ==
                                     Convert.ToInt32(username);
                }
            } catch(SqlException e) {
                Debug.WriteLine("Failed to check username: " + e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }

            // check logic - here we check if you are admin or it's your own reservation
            if(!ownReservation && !AuthorizationSystem.IsAdmin(username)) {
                return 0;
            } else {
                sqlCommand = new SqlCommand("DELETE FROM Reservations WHERE id=" + reservationId, sqlConnection);
                int i = -2;
                try {
                    i = sqlCommand.ExecuteNonQuery();
                } catch(Exception e) {
                    Debug.WriteLine(e.Message);
                }
                return i;
            }
        }

        public Reservation PostReservation(Reservation reservation) {

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Reservations VALUES(@username,@bus,@fromDate,@toDate); SELECT Scope_Identity();", sqlConnection);

            // Username
            SqlParameter sqlParameter = new SqlParameter("@username", SqlDbType.Int);
            sqlParameter.Value = reservation.User.Mobile;
            sqlCommand.Parameters.Add(sqlParameter);

            // Bus
            sqlParameter = new SqlParameter("@bus", SqlDbType.NVarChar);
            sqlParameter.Value = reservation.Bus.RegNo;
            sqlCommand.Parameters.Add(sqlParameter);

            // fromDate
            sqlParameter = new SqlParameter("@fromDate", SqlDbType.DateTime);
            sqlParameter.Value = reservation.FromDate;
            sqlCommand.Parameters.Add(sqlParameter);

            // toDate
            sqlParameter = new SqlParameter("@toDate", SqlDbType.DateTime);
            sqlParameter.Value = reservation.ToDate;
            sqlCommand.Parameters.Add(sqlParameter);

            int id = -1;
            try {
                id = Convert.ToInt32(sqlCommand.ExecuteScalar());
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return GetReservation(id);
        }

        public Reservation PostReservation(ReservationApiClass reservation) {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Reservations VALUES(@username,@bus,@fromDate,@toDate); SELECT Scope_Identity();", sqlConnection);

            // Username
            SqlParameter sqlParameter = new SqlParameter("@username", SqlDbType.Int);
            sqlParameter.Value = reservation.Mobile;
            sqlCommand.Parameters.Add(sqlParameter);

            // Bus
            sqlParameter = new SqlParameter("@bus", SqlDbType.NVarChar);
            sqlParameter.Value = reservation.RegNo;
            sqlCommand.Parameters.Add(sqlParameter);

            // fromDate
            sqlParameter = new SqlParameter("@fromDate", SqlDbType.DateTime);
            sqlParameter.Value = reservation.FromDate;
            sqlCommand.Parameters.Add(sqlParameter);

            // toDate
            sqlParameter = new SqlParameter("@toDate", SqlDbType.DateTime);
            sqlParameter.Value = reservation.ToDate;
            sqlCommand.Parameters.Add(sqlParameter);

            Reservation res = null;
            try {
                int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                res = GetReservation(id);
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return res;
        }

        public Reservation GetReservation(int id) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reservations WHERE id=" + id, sqlConnection);
            Reservation r = null;
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    r = new Reservation();
                    r.Id = Convert.ToInt32(reader[0]);
                    r.FromDate = Convert.ToDateTime(reader[3]);
                    r.ToDate = Convert.ToDateTime(reader[4]);
                    r.Bus = GetBus(Convert.ToString(reader[2]));
                    r.User = GetUser(Convert.ToInt32(reader[1]));
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                    reader.Close();
            }
            return r;
        }
    }
}
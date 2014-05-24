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

namespace WebAPI.Models {
    public class ReservationRepository : IReservationRespository {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private SqlParameter sqlParameter;

        public ReservationRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
        }

        public IEnumerable<Reservation> GetAllReservations() {
            sqlCommand = new SqlCommand("SELECT * FROM Reservations", sqlConnection);
            List<Reservation> reservations = null;
            try {
                reservations = new List<Reservation>();
                reader = sqlCommand.ExecuteReader();
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
            sqlCommand = new SqlCommand("SELECT * FROM Reservations r WHERE bus='AB12345' AND CONVERT(char(10),r.fromDate,126)=@date", sqlConnection);
            List<Reservation> list = new List<Reservation>();

            // regno
            sqlParameter = new SqlParameter("@regNo", SqlDbType.NVarChar);
            sqlParameter.Value = regNo;
            sqlCommand.Parameters.Add(sqlParameter);

            // date
            sqlParameter = new SqlParameter("@date", SqlDbType.DateTime);
            sqlParameter.Value = date;
            sqlCommand.Parameters.Add(sqlParameter);

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
                reader.Close();
                string query = sqlCommand.CommandText;

                foreach(SqlParameter p in sqlCommand.Parameters) {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }
                Debug.WriteLine(query);
            }
            return list;
        }

        public IEnumerable<Reservation> GetBusReservation(string regNo) {
            sqlCommand = new SqlCommand("SELECT * FROM Reservations r WHERE bus='AB12345';", sqlConnection);
            List<Reservation> list = new List<Reservation>();

            // regno
            sqlParameter = new SqlParameter("@regNo", SqlDbType.NVarChar);
            sqlParameter.Value = regNo;
            sqlCommand.Parameters.Add(sqlParameter);

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
                reader.Close();
            }
            return list;
        }


        private Bus GetBus(String regNo) {
            sqlCommand = new SqlCommand("SELECT * FROM Busses WHERE regNo='" + regNo + "'", sqlConnection);
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
            sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Mobile=" + id, sqlConnection);
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

        public bool DeleteReservation(int reservationId) {
            sqlCommand = new SqlCommand("DELETE FROM Reservations WHERE id=" + reservationId, sqlConnection);
            int i = -1;
            try {
                i = sqlCommand.ExecuteNonQuery();
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return i > 0;
        }

        public Reservation PostReservation(Reservation reservation) {

            sqlCommand = new SqlCommand("INSERT INTO Reservations VALUES(@username,@bus,@fromDate,@toDate); SELECT Scope_Identity();", sqlConnection);

            // Username
            sqlParameter = new SqlParameter("@username", SqlDbType.Int);
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

                Debug.WriteLine(id);
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return GetReservation(id);
        }

        public Reservation PostReservation(ReservationApiClass reservation) {

            sqlCommand = new SqlCommand("INSERT INTO Reservations VALUES(@username,@bus,@fromDate,@toDate); SELECT Scope_Identity();", sqlConnection);

            // Username
            sqlParameter = new SqlParameter("@username", SqlDbType.Int);
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

            int id = -1;
            try {
                id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                
                Debug.WriteLine(id);
                return GetReservation(id);
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
                return null;
            }
        }


        public Reservation GetReservation(int id) {
            sqlCommand = new SqlCommand("SELECT * FROM Reservations WHERE id=" + id, sqlConnection);
            Reservation r = null;
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
                reader.Close();
            }
            return r;
        }
    }
}
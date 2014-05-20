using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
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
            sqlCommand = new SqlCommand("SELECT * FROM Reservations",sqlConnection);
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

        private Bus GetBus(String regNo) {
            sqlCommand = new SqlCommand("SELECT * FROM Busses WHERE regNo='"+regNo+"'",sqlConnection);
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
                reader.Close();
            }
            return b;
        }

        private User GetUser(int id) {
            sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Mobile="+id,sqlConnection);
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
                reader.Close();
            }

            return u;
        }

        public void DeleteReservation(int reservationId) {
            sqlCommand = new SqlCommand("DELETE FROM Reservations WHERE id="+reservationId,sqlConnection);
            try {
                sqlCommand.ExecuteNonQuery();
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
        }

        public Reservation PostReservation(Reservation reservation) {
            sqlCommand = new SqlCommand("INSERT INTO Reservations VALUES(@username,@bus,@fromDate,@toDate);");

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
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }

            return GetReservation(id);
        }

        public Reservation GetReservation(int id) {
            sqlCommand = new SqlCommand("SELECT * FROM Reservations WHERE id="+id,sqlConnection);
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
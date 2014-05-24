using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Models;
using ModelSQL;

namespace WebAPI.Models {
    public class ComCenterRepository : ICenterRespository {
        private SqlConnection sqlConnection;

        public ComCenterRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
        }

        public IEnumerable<ComCenter> GetAllComCenters() {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ComCenters",sqlConnection);
            List<ComCenter> centers = new List<ComCenter>();
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    ComCenter cc = new ComCenter();
                    cc.Name = Convert.ToString(reader[0]);
                    cc.Address = Convert.ToString(reader[1]);
                    cc.ContactPerson = Convert.ToString(reader[2]);
                    cc.ContactPhone = Convert.ToInt32(reader[3]);
                    cc.Busses = GetBussesForCenter(cc.Name);
                    centers.Add(cc);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return centers;
        }

        public IEnumerable<Bus> GetBussesForCenter(string center) {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Busses WHERE comCenter =@center",sqlConnection);
            List<Bus> busses = new List<Bus>();
            // name
            SqlParameter sqlParameter = new SqlParameter("@center", SqlDbType.NVarChar);
            sqlParameter.Value = center;
            sqlCommand.Parameters.Add(sqlParameter);
            SqlDataReader reader = null;
            try {
                reader = sqlCommand.ExecuteReader();
                while(reader.Read()) {
                    Bus b = new Bus();
                    b.RegNo = Convert.ToString(reader[0]);
                    b.ComCenter = Convert.ToString(reader[1]);
                    busses.Add(b);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                if(reader != null)
                reader.Close();
            }
            return busses;
        } 
    }
}
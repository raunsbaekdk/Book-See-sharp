using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Models;
using ModelSQL;

namespace WebAPI.Models  {

    public class BusRepository : IBusRespository {
        private SqlConnection sqlConnection;

        public BusRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
        }

        public IEnumerable<Bus> GetAllBusses() {
            SqlCommand sqlCommand = new SqlCommand("select * from Busses",sqlConnection);
            SqlDataReader reader = null;
            List<Bus> busses = new List<Bus>();
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
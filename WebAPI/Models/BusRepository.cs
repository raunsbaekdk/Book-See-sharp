using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Models;

namespace WebAPI.Models  {

    public class BusRepository : IBusRespository {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;


        public BusRepository() {
            sqlConnection = Sql.GetInstance().GetConnection();
        }

        public IEnumerable<Bus> GetAllBusses() {
            sqlCommand = new SqlCommand("select * from Busses",sqlConnection);
            reader = sqlCommand.ExecuteReader();
            List<Bus> busses = new List<Bus>();
            try {
                while(reader.Read()) {
                    Bus b = new Bus();
                    b.RegNo = Convert.ToString(reader[0]);
                    b.ComCenter = Convert.ToString(reader[1]);
                    busses.Add(b);
                }
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            } finally {
                reader.Close();
            }
            return busses;
        }
    }
}
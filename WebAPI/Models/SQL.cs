using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Models {
    public class Sql {
        private static Sql sql;
        private static SqlConnection sqlConnection;
        private const String ConnectionString = "Data Source=gim.dk;database=Sum;User id=Sum;Password=sum1234;MultipleActiveResultSets=true";

        private Sql() {
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
        }

        public static Sql GetInstance() {
            return sql ?? (sql = new Sql());
        }

        public SqlConnection GetConnection() {
            return sqlConnection;
        }
    }
}
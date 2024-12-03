using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml.Automation.Peers;

namespace MedSy.Services.Prescription
{
    public class PrescriptionSqlDao:IPrescriptionDao
    {
        public SqlConnection ConnectSql()
        {
            // Kết nối database
            var connectionString = """
                Server = localhost;
                Database = medsy;
                User ID = sa;
                Password = SqlServer@123;
                TrustServerCertificate = True;
                """;

            var connection = new SqlConnection(connectionString);
            return connection;

        }
    }
}

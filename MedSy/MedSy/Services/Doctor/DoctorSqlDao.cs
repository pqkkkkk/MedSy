using MedSy.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace MedSy.Services.Doctor
{
    public class DoctorSqlDao : IDoctorDao
    {
        public List<Models.Doctor> getDoctors()
        {
            var result = new List<Models.Doctor>();

            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                TrustServerCertificate=True;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            var doctorSql = """"
                SELECT *
                FROM doctor
                """";
            var doctorCommand = new MySqlCommand(doctorSql);
            var reader = doctorCommand.ExecuteReader();

            while(reader.Read())
            {
                var doctor = new Models.Doctor();
                doctor.id = (int)reader["id"];
                doctor.username = (string)reader["username"];
                doctor.password = "";
                doctor.avatarPath = (string)reader["avatarPath"];

                result.Add(doctor);
            }

            

            return result;
        }
    }
}

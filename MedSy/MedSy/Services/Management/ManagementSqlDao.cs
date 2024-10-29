using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Management
{
    public class ManagementSqlDao : IManagementDao
    {
        public List<Models.Doctor> getDoctors(int patientId)
        {
            var result = new List<Models.Doctor>();

            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            var doctorsSql = """
                SELECT d.id as doctorId, d.avatarPath as doctorAvatar, m.doctorNewMessage as newMessage , d.username as username
                FROM management m join doctor d on m.doctorId = d.id
                WHERE patientId = @pi
                """;
            var doctorsCommand = new MySqlCommand(doctorsSql, connection);
            doctorsCommand.Parameters.Add("@pi", MySqlDbType.Int64)
                .Value = patientId;
            var reader = doctorsCommand.ExecuteReader();

            while (reader.Read())
            {
                var doctor = new Models.Doctor();
                doctor.id = (int)reader["doctorId"];
                doctor.username = (string)reader["username"];
                doctor.avatarPath = (string)reader["doctorAvatar"];
                doctor.newMessage = (bool)reader["newMessage"];
                doctor.password = "";

                result.Add(doctor);
            }
            connection.Close();

            return result;
        }

        public void offDoctorNewMessageNotify(int patientId, int doctorId)
        {
            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            var sql = """
                UPDATE management set doctorNewMessage = false where (patientId=@pi and doctorId=@di)
                """;
            var command = new MySqlCommand(sql, connection);
            command.Parameters.Add("@pi", MySqlDbType.Int64)
                .Value = patientId;
            command.Parameters.Add("@di", MySqlDbType.Int64)
                .Value = doctorId;
            command.ExecuteNonQuery();

            connection.Close();
        }
        public void onDoctorNewMessageNotify(int patientId, int doctorId)
        {
            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            var sql = """
                UPDATE management set doctorNewMessage = true where (patientId=@pi and doctorId=@di)
                """;
            var command = new MySqlCommand(sql, connection);
            command.Parameters.Add("@pi", MySqlDbType.Int64)
                .Value = patientId;
            command.Parameters.Add("@di", MySqlDbType.Int64)
                .Value = doctorId;
            command.ExecuteNonQuery();

            connection.Close();
        }

    }
}

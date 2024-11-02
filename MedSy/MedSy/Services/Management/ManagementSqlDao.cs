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
        public List<Models.User> getConnectingUsers(int currentUserId, string currentRole)
        {
            var result = new List<Models.User>();

            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                SELECT u.id as doctorId, u.avatarPath as doctorAvatar, m.doctorNewMessage as newMessage , u.username as username
                FROM management m join systemuser u on m.doctorId = u.id
                WHERE patientId = @cui
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("@cui", MySqlDbType.Int64)
                    .Value = currentUserId;
                var reader = command.ExecuteReader();

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
                
            }
            else if (currentRole == "doctor")
            {
                var sql = """
                SELECT u.id as patientId, u.avatarPath as patientAvatar, m.patientNewMessage as newMessage , u.username as username
                FROM management m join systemuser u on m.patientId = u.id
                WHERE doctorId = @cui
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("@cui", MySqlDbType.Int64)
                    .Value = currentUserId;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var patient = new Models.Patient();
                    patient.id = (int)reader["patientId"];
                    patient.username = (string)reader["username"];
                    patient.avatarPath = (string)reader["patientAvatar"];
                    patient.newMessage = (bool)reader["newMessage"];
                    patient.password = "";

                    result.Add(patient);
                }
            }

            connection.Close();
            return result;
        }
        public void offNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                UPDATE management set doctorNewMessage = false where (patientId=@cui and doctorId=@oui)
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("cui", MySqlDbType.Int64)
                    .Value = currentUserId;
                command.Parameters.Add("@oui", MySqlDbType.Int64)
                    .Value = oppositeUserId;
                command.ExecuteNonQuery();
            }
            else
            {
                var sql = """
                UPDATE management set patientNewMessage = false where (patientId=@oui and doctorId=@cui)
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("@oui", MySqlDbType.Int64)
                    .Value = oppositeUserId;
                command.Parameters.Add("@cui", MySqlDbType.Int64)
                    .Value = currentUserId;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void onNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                UPDATE management set doctorNewMessage = true where (patientId=@cui and doctorId=@oui)
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("cui", MySqlDbType.Int64)
                    .Value = currentUserId;
                command.Parameters.Add("@oui", MySqlDbType.Int64)
                    .Value = oppositeUserId;
                command.ExecuteNonQuery();
            }
            else
            {
                var sql = """
                UPDATE management set patientNewMessage = true where (patientId=@oui and doctorId=@cui)
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("@oui", MySqlDbType.Int64)
                    .Value = oppositeUserId;
                command.Parameters.Add("@cui", MySqlDbType.Int64)
                    .Value = currentUserId;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public int checkNewMessage(int currentUserId, string currentRole)
        {
            int result = 0;

            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                SELECT count(*) as NewMessageCount FROM management m
                where m.patientId = @cui and doctorNewMessage = true
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("cui", MySqlDbType.Int64)
                    .Value = currentUserId;

                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    result = Convert.ToInt32(reader["NewMessageCount"]);
                }
            }
            else
            {
                var sql = """
                SELECT count(*) as NewMessageCount FROM management m
                where m.doctorId = @cui and patientNewMessage = true
                """;
                var command = new MySqlCommand(sql, connection);
                command.Parameters.Add("@cui", MySqlDbType.Int64)
                    .Value = currentUserId;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = Convert.ToInt32(reader["NewMessageCount"]);
                }
            }
            connection.Close();

            return result;
        }
    }
}

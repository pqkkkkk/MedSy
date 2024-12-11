using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;
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
        private SqlConnection connection;
        public ManagementSqlDao()
        {
            connection = (Application.Current as App).locator.sqlConnection;
        }
        public List<Models.User> getConnectingUsers(int currentUserId, string currentRole)
        {
            var result = new List<Models.User>();

            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                SELECT u.id as doctorId, m.doctor_New_Message as newMessage , u.fullname as fullname, u.role as role, u.avatar_Path as avatarPath
                FROM management m join users u on m.doctor_Id = u.id
                WHERE patient_Id = @cui
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cui", currentUserId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var doctor = new Models.User();
                    doctor.id = (int)reader["doctorId"];
                    doctor.fullName = (string)reader["fullname"];
                    doctor.newMessage = (bool)reader["newMessage"];
                    doctor.role = (string)reader["role"];
                    doctor.avatarPath = (string)reader["avatarPath"];
                    result.Add(doctor);
                }
                
            }
            else if (currentRole == "doctor")
            {
                var sql = """
                SELECT u.id as patientId, m.patient_New_Message as newMessage , u.fullname as fullname, u.role as role, u.avatar_Path as avatarPath
                FROM management m join users u on m.patient_Id = u.id
                WHERE doctor_Id = @cui
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cui", currentUserId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var patient = new Models.User();
                    patient.id = (int)reader["patientId"];
                    patient.fullName = (string)reader["fullname"];
                    patient.newMessage = (bool)reader["newMessage"];
                    patient.role = (string)reader["role"];
                    patient.avatarPath = (string)reader["avatarPath"];
                    result.Add(patient);
                }
            }

            connection.Close();
            return result;
        }
        public void offNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                UPDATE management set doctor_New_Message = 0 where (patient_Id=@cui and doctor_Id=@oui)
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("cui", currentUserId);
                command.Parameters.AddWithValue("@oui", oppositeUserId);
                command.ExecuteNonQuery();
            }
            else
            {
                var sql = """
                UPDATE management set patient_New_Message = 0 where (patient_Id=@oui and doctor_Id=@cui)
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@oui", oppositeUserId);
                command.Parameters.AddWithValue("@cui",currentUserId);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void onNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
 
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                UPDATE management set doctor_New_Message = 1 where (patient_Id=@cui and doctor_Id=@oui)
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("cui", currentUserId);
                command.Parameters.AddWithValue("@oui", oppositeUserId);
                command.ExecuteNonQuery();
            }
            else
            {
                var sql = """
                UPDATE management set patient_New_Message = 1 where (patient_Id=@oui and doctor_Id=@cui)
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@oui", oppositeUserId);
                command.Parameters.AddWithValue("@cui", currentUserId);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public int checkNewMessage(int currentUserId, string currentRole)
        {
            int result = 0;

            connection.Open();
            if (currentRole == "patient")
            {
                var sql = """
                SELECT count(*) as NewMessageCount FROM management m
                where m.patient_Id = @cui and doctor_New_Message = 1
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("cui", currentUserId);

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
                where m.doctor_Id = @cui and patient_New_Message = 1
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cui", currentUserId);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = Convert.ToInt32(reader["NewMessageCount"]);
                }
            }
            connection.Close();

            return result;
        }

        public void onMySelfNewMessageNotify(int currentUserId, int oppositeUserId, string currentRole)
        {
            connection.Open();

            if (currentRole == "patient")
            {
                var sql = """
                UPDATE management set patient_New_Message = 1 where (patient_Id=@cui and doctor_Id=@oui)
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("cui", currentUserId);
                command.Parameters.AddWithValue("@oui", oppositeUserId);
                command.ExecuteNonQuery();
            }
            else
            {
                var sql = """
                UPDATE management set doctor_New_Message = 1 where (patient_Id=@oui and doctor_Id=@cui)
                """;
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@oui", oppositeUserId);
                command.Parameters.AddWithValue("@cui", currentUserId);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}

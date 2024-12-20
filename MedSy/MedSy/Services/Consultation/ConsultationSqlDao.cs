using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppNotifications.Builder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Windows.Networking.Proximity;
using Windows.System;

namespace MedSy.Services.Consultation
{
    public class ConsultationSqlDao:IConsultationDao
    {
        private SqlConnection connection;
        private List<Models.Consultation> consultations;
        public ConsultationSqlDao()
        {
            connection = (Application.Current as App).locator.sqlConnection;
        }

        public List<Models.Consultation> GetConsultations(string userRole, int userId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            consultations = new List<Models.Consultation>();
            
            connection.Open();

            var sqlQuery = new StringBuilder("SELECT * FROM consultation where 1=1");
                

            if (userRole == "patient")
            {
                sqlQuery.Append(" AND patient_id = @userId");
            }
            else if (userRole == "doctor")
            {
                sqlQuery.Append(" AND doctor_id = @userId ");
            }

            if (status != null && status != "All")
            {
                sqlQuery.Append(" AND status = @status");
            }

            if (date.HasValue)
            {
                sqlQuery.Append(" AND date = @date ");
            }
            if (startTime.HasValue)
            {
                sqlQuery.Append(" AND start_time >= @startTime ");
            }
            if (endTime.HasValue)
            {
                sqlQuery.Append(" AND end_time <= @endTime ");
            }
            var command = new SqlCommand(sqlQuery.ToString(), connection);

            command.Parameters.AddWithValue("@userId", userId);
            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                command.Parameters.AddWithValue("@status", status);
            }
            if (date.HasValue)
            {
                command.Parameters.AddWithValue("@date", date.Value.ToDateTime(TimeOnly.MinValue));
            }
            if (startTime.HasValue)
            {
                command.Parameters.AddWithValue("@startTime", startTime.Value.ToTimeSpan());
            }
            if (endTime.HasValue)
            {
                command.Parameters.AddWithValue("@endTime", endTime.Value.ToTimeSpan());
            }

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var consultation = new Models.Consultation
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("date"))),
                            startTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("start_time"))),
                            endTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("end_time"))),
                            form = reader.GetString(reader.GetOrdinal("type")),
                            status = reader.GetString(reader.GetOrdinal("status")),
                            patientId = reader.GetInt32(reader.GetOrdinal("patient_id")),
                            doctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                            result = (reader.IsDBNull(reader.GetOrdinal("consultation_result"))) ? "None" : reader.GetString(reader.GetOrdinal("consultation_result")),
                            reason = reader.IsDBNull(reader.GetOrdinal("reason")) ? null : reader.GetString(reader.GetOrdinal("reason"))
                        };
                        if(consultation.result =="")
                        {
                            consultation.result = "None";
                        }
                        consultations.Add(consultation);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        
            connection.Close();
            return consultations;
        }
        public List<Models.Consultation> GetConsultationsInAWeek(int doctorId, DateOnly? date)
        {
            consultations = new List<Models.Consultation>();
            if (date.HasValue)
            {
                int dayOfWeek = (int)date.Value.DayOfWeek;
                DateOnly sunday = date.Value.AddDays(-dayOfWeek);
                DateOnly saturday = sunday.AddDays(6);

                var sqlQuery = $"""
                    SELECT *
                    FROM consultation
                    WHERE doctor_id = @doctorId
                    AND status = 'Accepted'
                    AND date >= @sunday AND date <= @saturday
                """;
                var command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@doctorId", doctorId);
                command.Parameters.AddWithValue("@sunday", sunday.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@saturday", saturday.ToString("yyyy-MM-dd"));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var consultation = new Models.Consultation
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("date"))),
                                startTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("start_time"))),
                                endTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("end_time"))),
                                form = reader.GetString(reader.GetOrdinal("type")),
                                status = reader.GetString(reader.GetOrdinal("status")),
                                patientId = reader.GetInt32(reader.GetOrdinal("patient_id")),
                                doctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                                result = reader.IsDBNull(reader.GetOrdinal("consultation_result")) ? null : reader.GetString(reader.GetOrdinal("consultation_result")),
                                reason = reader.IsDBNull(reader.GetOrdinal("reason")) ? null : reader.GetString(reader.GetOrdinal("reason"))
                            };

                            consultations.Add(consultation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }

            return consultations;



        }

        public Dictionary<string, int> GetPathologyCountByMonth(int month)
        {
            var pathologyCount = new Dictionary<string, int>();

            string query = @"
            SELECT p.name, COUNT(c.id) AS Count
            FROM pathology p LEFT JOIN consultation c
            ON p.name = c.pathology AND MONTH(c.date) = @month
            GROUP BY p.name";

            try
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@month", month);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string pathology = reader.GetString(0);
                            int count = reader.GetInt32(1);

                            pathologyCount[pathology] = count;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return pathologyCount;
        }

        public Dictionary<int,int> countOnlineConsultationEachMonth(int year)
        {
            var OnlineConsultationEachMonth = new Dictionary<int, int>();
            connection.Open();
            var query = $"""
                SELECT m.month, COUNT(c.id) AS count
                FROM (VALUES 
                      (1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11), (12)
                     ) AS m(month)
                LEFT JOIN consultation c
                    ON MONTH(c.date) = m.month
                    AND YEAR(c.date) = @year
                    AND c.type = 'online'
                GROUP BY m.month
                ORDER BY m.month;
                """;

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@year", year);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int month = reader.GetInt32(0);
                        int count = reader.GetInt32(1);

                        OnlineConsultationEachMonth[month] = count;
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return OnlineConsultationEachMonth;
        }
        public SqlConnection ConnectSql()
        {
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

        public List<string> getAllPathology()
        {
            connection.Open();
            var query = $"""
                select * from pathology
                """;
            var command = new SqlCommand(query, connection);
            List<string> pathologies = new List<string>();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pathologies.Add(reader.GetString(reader.GetOrdinal("name")));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return pathologies;
        }

        public bool createConsultation(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime, string form, string status, int patientId, int doctorId, string consultation_result, string reason, string pathology)
        {
            connection.Open();

            var query = $"""
                INSERT INTO consultation (date, start_time, end_time, type, status, patient_id, doctor_id, consultation_result, reason, pathology)
                VALUES (@date, @startTime, @endTime, @form, @status, @patientId, @DoctorId, @consultation_result, @reason, @pathology)
                """;

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@date", date.HasValue ? date.Value.ToString("yyyy-MM-dd") : DBNull.Value);
            command.Parameters.AddWithValue("@startTime", startTime.HasValue ? startTime.Value.ToString() : DBNull.Value);
            command.Parameters.AddWithValue("@endTime", endTime.HasValue ? endTime.Value.ToString() : DBNull.Value);
            command.Parameters.AddWithValue("@form", form);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@patientId", patientId);
            command.Parameters.AddWithValue("@doctorId", doctorId);
            command.Parameters.AddWithValue("@consultation_result", string.IsNullOrEmpty(consultation_result) ? DBNull.Value : consultation_result);
            command.Parameters.AddWithValue("@reason", string.IsNullOrEmpty(reason) ? DBNull.Value : reason);
            command.Parameters.AddWithValue("@pathology", pathology);

            try
            {
                
                int result = command.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false; 
            }
            finally
            {
                connection.Close(); 
            }
        }
        public Models.Consultation GetNextConsultationToday(string userRole, int userId)
        {
            var list = GetConsultations(userRole, userId, "Accepted", DateOnly.FromDateTime(DateTime.Now), null, null);
            Models.Consultation result = null;
            if (list.Count != 0)
            {
                list.OrderBy(c => c.startTime).ToList();
                result = list.FirstOrDefault();
            }
            return result;
        }
        public int UpdateStatus(Models.Consultation selectedConsultation, string status)
        {
            connection.Open();

            var query = $"""
                UPDATE consultation set status = @status where id = @id
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@id", selectedConsultation.id);
            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();
            return rowsAffected;
        }
        public int DeleteConsultation(Models.Consultation selectedConsultation)
        {
            connection.Open();

            var query = $"""
                DELETE FROM consultation where id = @id;
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", selectedConsultation.id);
            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();
            return rowsAffected;

        }
        public bool updateResult(int id, string result)
        {
            connection.Open();

            var query = $"""
                UPDATE consultation set consultation_result = @result where id = @id
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@result", result);
            command.Parameters.AddWithValue("@id", id);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                connection.Close();
                return true;
            }
            return false;

        }
        public int UpdateAllMissedConsultations(string userRole, int userId)
        {
            string sql = "";
            if (userRole == "doctor")
            {
                sql = """
                UPDATE consultation
                SET status = 'Missed'
                WHERE date = @currentDate AND end_time < @Now  AND status = 'Accepted' AND doctor_id = @userId
                """;
            }
            else if (userRole == "patient")
            {
                sql = """
                UPDATE consultation
                SET status = 'Missed'
                WHERE date = @currentDate AND end_time < @Now  AND status = 'Accepted' AND patient_id = @userId
                """;
            }
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@currentDate", DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Now", TimeOnly.FromDateTime(DateTime.Now).ToString());
            command.Parameters.AddWithValue("@userId", userId);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected;
        }
        public int UpdateStatusToDone(Models.Consultation selectedConsultation)
        {
            throw new NotImplementedException();
        }

        public List<Models.Consultation> GetAllDoneConsultationsByDoctorIdAndPatientId(int doctorId, int patientId)
        {
            consultations = new List<Models.Consultation>();
            connection.Open();

            var sql = """
                SELECT * FROM consultation
                WHERE doctor_id = @doctorId AND patient_id = @patientId and status = 'Done'
                """;
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@doctorId", doctorId);
            command.Parameters.AddWithValue("@patientId", patientId);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var consultation = new Models.Consultation
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("date"))),
                            startTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("start_time"))),
                            endTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("end_time"))),
                            form = reader.GetString(reader.GetOrdinal("type")),
                            status = reader.GetString(reader.GetOrdinal("status")),
                            patientId = reader.GetInt32(reader.GetOrdinal("patient_id")),
                            doctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                            result = (reader.IsDBNull(reader.GetOrdinal("consultation_result"))) ? "None" : reader.GetString(reader.GetOrdinal("consultation_result")),
                            reason = reader.IsDBNull(reader.GetOrdinal("reason")) ? null : reader.GetString(reader.GetOrdinal("reason"))
                        };
                        if (consultation.result == "")
                        {
                            consultation.result = "None";
                        }
                        consultations.Add(consultation);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            connection.Close();
            return consultations;
        }
    }

}

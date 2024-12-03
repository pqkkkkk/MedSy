using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using Microsoft.Data.SqlClient;

namespace MedSy.Services.Consultation
{
    public class ConsultationSqlDao:IConsultationDao
    {
        private List<Models.Consultation> consultations;

        public List<Models.Consultation> GetConsultations(int doctorId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            SqlConnection connection = ConnectSql();

            var sqlQuery = $"""
                SELECT *
                FROM consultation where doctor_id = @doctorId and status = @status and date = @date 
                """;

            if (startTime.HasValue && endTime.HasValue)
            {
                sqlQuery += " AND start_time >= @startTime AND end_time <= @endTime";
            }

            var command = new SqlCommand(sqlQuery, connection);

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

            connection.Close();
            return consultations;
        }
        public List<Models.Consultation> GetConsultationsInAWeek(int doctorId, DateOnly? date)
        {
            if (date.HasValue)
            {
                // Tính toán ngày chủ nhật và ngày thứ bảy trong tuần của ngày đã cho
                int dayOfWeek = (int)date.Value.DayOfWeek;
                DateOnly sunday = date.Value.AddDays(-dayOfWeek);
                DateOnly saturday = sunday.AddDays(6);

                var connection = ConnectSql();

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
                    connection.Open(); // Mở kết nối
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

                            consultations.Add(consultation); // Thêm vào danh sách kết quả
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
        public SqlConnection ConnectSql()
        {
            // Kết nối database
            var connectionString = """
                Server = localhost:1433;
                Database = medsy;
                User ID = sa;
                Password = SqlServer@123;
                TrustServerCertificate = True;
                """;

            var connection = new SqlConnection(connectionString);
            return connection;

        }

        public bool createConsultation(DateOnly? date, TimeOnly? startTime, TimeOnly? endTime, String form, String status, int patientId, int doctorId, String consultation_result, String reason)
        {
            var connection = ConnectSql();

            var query = $"""
                INSERT INTO consultation (date, start_time, end_time, type, status, patient_id, doctor_id, consultation_result, reason)
                VALUES (@date, @startTime, @endTime, @form, @status, @patientId, @DoctorId, @consultation_result, @reason)
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
            try
            {
                connection.Open(); // Mở kết nối
                int result = command.ExecuteNonQuery(); // Thực thi câu lệnh INSERT

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

        public List<Models.Consultation> GetConsultations(string userRole, int userId, string status, DateOnly? date, TimeOnly? startTime, TimeOnly? endTime)
        {
            throw new NotImplementedException();
        }

        public int UpdateStatusToDone(Models.Consultation selectedConsultation)
        {
            throw new NotImplementedException();
        }

        public Models.Consultation GetNextConsultationToday(string userRole, int userId)
        {
            throw new NotImplementedException();
        }
    }

}

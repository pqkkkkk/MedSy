using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.Drug;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;
using Windows.System;

namespace MedSy.Services.Prescription
{
    public class PrescriptionSqlDao:IPrescriptionDao
    {
        private SqlConnection connection;
        public List<Models.Prescription> prescriptions { get; set; }
        public PrescriptionSqlDao()
        {
            connection = (Application.Current as App).locator.sqlConnection;
        }
        public bool createPrescription(int totalprice, DateOnly createdDay, int consultationId)
        {
            
            connection.Open();
            try
            {
                var query = $"""
                INSERT INTO prescription(total_price, created_day, consultation_id, status) values (@totalprice, @createdDay, @consultationId, @status)
                """;

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@totalprice", totalprice);
                command.Parameters.AddWithValue("@createdDay", createdDay.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@consultationId", consultationId);
                command.Parameters.AddWithValue("@status", "unpaid");
                var rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating prescription: " + ex.Message);
                connection.Close();
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public List<Models.Prescription> GetPrescriptions(int userId, string status)
        {
            prescriptions = new List<Models.Prescription>();
            var query = $"""
                select * from prescription p join consultation c on p.consultation_id = c.id
                                             join users u on u.id = c.patient_id
                Where u.id = @userId
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            try
            {
                connection.Open();
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prescription = new Models.Prescription()
                        {
                            prescriptionId = reader.GetInt32(reader.GetOrdinal("id")),
                            created_day = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("created_day"))),
                            totalPrice = reader.GetInt32(reader.GetOrdinal("total_price")),
                            consultationId = reader.GetInt32(reader.GetOrdinal("consultation_id")),
                            status = reader.GetString(reader.GetOrdinal("status"))
                        };
                        prescriptions.Add(prescription);
                    }
                }

                prescriptions = prescriptions.Where(p => p.status == status).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return prescriptions;
        }
        public List<Models.PrescriptionDetail> getPrescriptionDetails_ByPrescriptionId(int prescriptionId)
        {
            List<PrescriptionDetail> prescriptionDetails = new List<PrescriptionDetail>();
            var query = $"""
                select * from prescription_detail where prescription_id = @prescriptionId
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@prescriptionId", prescriptionId);

            try
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prescriptionDetail = new PrescriptionDetail()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            usage = reader.GetString(reader.GetOrdinal("usage")),
                            prescription_id = reader.GetInt32(reader.GetOrdinal("prescription_id")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            drug_id = reader.GetInt32(reader.GetOrdinal("drug_id"))
                        };
                        prescriptionDetails.Add(prescriptionDetail);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return prescriptionDetails;
        }
        public void insertIntoPrescriptionDetail(Models.PrescriptionDetail prescriptionDetail, int prescriptionId)
        {
            
            var query = $"""
                        INSERT INTO prescription_detail (quantity, usage, prescription_id, drug_id, price)
                        VALUES (@quantity, @usage, @prescriptionId, @drugId, @price)
                        """;

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@price", prescriptionDetail.price);
            command.Parameters.AddWithValue("@quantity", prescriptionDetail.quantity);
            command.Parameters.AddWithValue("@usage", prescriptionDetail.usage);
            command.Parameters.AddWithValue("@prescriptionId", prescriptionId);
            command.Parameters.AddWithValue("@drugId", prescriptionDetail.drug.drugId);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Prescription detail inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to insert prescription detail.");
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
        public int getPrescriptionId_ByConsultationId(int consultationId)
        {
            var query = $"""
                select id from prescription where consultation_id = @consultationId
                """;

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@consultationId", consultationId);
            try
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        connection.Close();
                        return id;
                    }
                }

                connection.Close();
                Console.WriteLine("Prescription not found.");
                return -1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                connection.Close();
                return -1;
            }
        }
        public int updateTotalPrice(int totalprice, int prescriptionId)
        {
            connection.Open();

            var query = $"""
                UPDATE prescription set total_price = @totalprice where id = @prescriptionId
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@totalprice", totalprice);
            command.Parameters.AddWithValue("@prescriptionId", prescriptionId);
            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();
            return rowsAffected;
        }
        public List<PrescriptionDetail> getPrescriptionDetails(int consultationId)
        {
            List<Models.PrescriptionDetail> prescriptionDetails = new List<Models.PrescriptionDetail>();
            var query = $"""
                select pd.id as id, pd.quantity as quantity, pd.usage as usage, pd.prescription_id as prescription_id, pd.drug_id as drug_id, pd.price as price
                FROM prescription_detail pd
                JOIN prescription p on pd.prescription_id = p.id
                JOIN consultation c on p.consultation_id = c.id
                where c.id = @consultationId
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@consultationId", consultationId);

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();    
                while (reader.Read())
                {
                    Models.PrescriptionDetail prescriptionDetail = new Models.PrescriptionDetail();
                    prescriptionDetail.id = reader.GetInt32(reader.GetOrdinal("id"));
                    prescriptionDetail.quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                    prescriptionDetail.usage = reader.GetString(reader.GetOrdinal("usage"));
                    prescriptionDetail.prescription_id = reader.GetInt32(reader.GetOrdinal("prescription_id"));
                    prescriptionDetail.price = reader.GetInt32(reader.GetOrdinal("price"));
                    prescriptionDetail.drug_id = reader.GetInt32(reader.GetOrdinal("drug_id"));
                    prescriptionDetails.Add(prescriptionDetail);
                }
                    return prescriptionDetails;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return prescriptionDetails;
            }
            finally
            {
                connection.Close();
            }
            
        }
        public int UpdatePrescriptionStatus(int prescriptionId)
        {
            connection.Open();

            var query = $"""
                UPDATE prescription set status = @status where id = @prescriptionId
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@status", "paid");
            command.Parameters.AddWithValue("@prescriptionId", prescriptionId);
            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();
            return rowsAffected;
        }
        public Dictionary<int,int> calculateRevenueByYear(int year)
        {
            connection.Open();
            var revenue = new Dictionary<int, int>();
            var query = $"""
                select month(created_day) as month, sum(total_price) as total
                from prescription
                where year(created_day) = @year and status = 'Paid'
                group by month(created_day)
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@year", year);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        revenue.Add(reader.GetInt32(reader.GetOrdinal("month")), reader.GetInt32(reader.GetOrdinal("total")));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return revenue;
        }
        public Dictionary<int, int> calculateRevenueByMonth(int month, int year)
        {
            connection.Open();
            var revenue = new Dictionary<int, int>();
            var query = $"""
                SELECT DATEPART(WEEK, created_day) - DATEPART(WEEK, DATEADD(DAY, 1 - DAY(created_day), created_day)) + 1 AS week,SUM(total_price) AS total
                FROM prescription
                WHERE MONTH(created_day) = 12 AND YEAR(created_day) = 2024 AND status = 'Paid'
                GROUP BY DATEPART(WEEK, created_day) - DATEPART(WEEK, DATEADD(DAY, 1 - DAY(created_day), created_day)) + 1
                ORDER BY week;
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@month", month);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        revenue.Add(reader.GetInt32(reader.GetOrdinal("week")), reader.GetInt32(reader.GetOrdinal("total")));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return revenue;
        }
        public Dictionary<int, int> calculateRevenueByWeek(int week, int month, int year)
        {
            connection.Open();
            var revenue = new Dictionary<int, int>();
            var query = $"""
                SELECT DATEPART(WEEKDAY, created_day) AS day, SUM(total_price) AS total
                FROM prescription
                WHERE MONTH(created_day) = @month AND YEAR(created_day) = @year AND status = 'Paid' AND DATEPART(WEEK, created_day) - DATEPART(WEEK, DATEADD(DAY, 1 - DAY(created_day), created_day)) + 1 = @week
                GROUP BY DATEPART(WEEKDAY, created_day)
                ORDER BY day;
                """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@month", month);
            command.Parameters.AddWithValue("@week", week);

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        revenue.Add(
                            reader.GetInt32(reader.GetOrdinal("day")),
                            reader.GetInt32(reader.GetOrdinal("total"))
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            connection.Close();
            return revenue;
        }
    }
}

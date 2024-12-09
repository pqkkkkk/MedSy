using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MedSy.Services.Prescription
{
    public class PrescriptionSqlDao:IPrescriptionDao
    {
        public bool createPrescription(int totalprice, DateOnly createdDay, int consultationId)
        {
            var connection = ConnectSql();
            connection.Open();
            try
            {


                var query = $"""
                INSERT INTO prescription(total_price, created_day, consultation_id) values (@totalprice, @createdDay, @consultationId)
                """;

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@totalprice", totalprice);
                command.Parameters.AddWithValue("@createdDay", createdDay.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@consultationId", consultationId);

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

        public void insertIntoPrescriptionDetail(int quantity, string usage, int prescriptionId, int drugId)
        {
            var connection = ConnectSql();

            var query = $"""
        INSERT INTO prescription_detail (quantity, usage, prescription_id, drug_id)
        VALUES (@quantity, @usage, @prescriptionId, @drugId)
        """;

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.Parameters.AddWithValue("@usage", usage);
            command.Parameters.AddWithValue("@prescriptionId", prescriptionId);
            command.Parameters.AddWithValue("@drugId", drugId);

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
        public int getPrescriptionId_ByConsultationId(int consultationId)
        {
            var connection = ConnectSql();

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
            var connection = ConnectSql();
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

    }
}

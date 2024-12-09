using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MedSy.Services.Feedback;
public class FeedbackSqlDao : IFeedbackDao
{
    private List<Models.Feedback> feedbacks;
    public List<Models.Feedback> GetFeedback()
    {
        feedbacks = new List<Models.Feedback>();
        var connection = ConnectSql();

        connection.Open();
        var query = $"""
                select * from feedback
                """;

        var command = new SqlCommand(query, connection);
        try
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var feedback = new Models.Feedback()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        DoctorID = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                        PatientID = reader.GetInt32(reader.GetOrdinal("patient_id")),
                        Content = reader.GetString(reader.GetOrdinal("content")),
                        Rating = reader.GetDouble(reader.GetOrdinal("rating"))
                    };
                    feedbacks.Add(feedback);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        connection.Close();
        return feedbacks;
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

}
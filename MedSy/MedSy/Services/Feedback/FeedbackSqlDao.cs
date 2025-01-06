using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;

namespace MedSy.Services.Feedback;
public class FeedbackSqlDao : IFeedbackDao
{
    private SqlConnection connection;
    private List<Models.Feedback> feedbacks;
    public FeedbackSqlDao()
    {
        connection = (Application.Current as App).locator.sqlConnection;
    }
    public List<Models.Feedback> GetFeedback()
    {
        feedbacks = new List<Models.Feedback>();

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
    public void AddFeedback(Models.Feedback feedback)
    {
        connection.Open();
        var query = $"""
                insert into feedback (doctor_id, patient_id, content, rating)
                values ({feedback.DoctorID}, {feedback.PatientID}, '{feedback.Content}', {feedback.Rating})
                """;

        var command = new SqlCommand(query, connection);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        connection.Close();
    }



}
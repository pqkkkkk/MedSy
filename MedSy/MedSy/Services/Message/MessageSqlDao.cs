using MedSy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.UI.Xaml;
namespace MedSy.Services.Message
{
    public class MessageSqlDao : IMessageDao
    {
        private SqlConnection connection;
        public MessageSqlDao()
        {

            connection = (Application.Current as App).locator.sqlConnection;
        }
        public List<Models.Message> getMessages(int currentUserId, int oppositeUserId)
        {
            var result = new List<Models.Message>();
            connection.Open();

            var messageSql = $"""
                SELECT content, sender_Id, receiver_Id
                FROM message
                WHERE (sender_Id=@patientId and receiver_Id=@doctorId) 
                      or (sender_Id=@doctorId and receiver_Id=@patientId)
                """;
            var messageCommand = new SqlCommand(messageSql, connection);
            messageCommand.Parameters.AddWithValue("@patientId", currentUserId);
            messageCommand.Parameters.AddWithValue("@doctorId", oppositeUserId);

            var reader = messageCommand.ExecuteReader();
            while(reader.Read())
            {
                var message = new Models.Message();
                message.senderId = (int)reader["sender_Id"];
                message.receiverId = (int)reader["receiver_Id"];
                message.content = (string)reader["content"];
                result.Add(message);
            }

            connection.Close();
            return result;
        }
        public int addMessage(int senderId, int receiverId, string content)
        {
            
            connection.Open();

            var sql = $"""
                INSERT INTO message(sender_Id,receiver_Id,content) VALUES (@si,@ri,@c)
                """;
            var messageCommand = new SqlCommand(sql, connection);
            messageCommand.Parameters.AddWithValue("@si", senderId);
            messageCommand.Parameters.AddWithValue("ri", receiverId);
            messageCommand.Parameters.AddWithValue("@c", content);

            int result = messageCommand.ExecuteNonQuery();
            

            connection.Close();
            return result;
        }
    }
}

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
namespace MedSy.Services.Message
{
    public class MessageSqlDao : IMessageDao
    {
        public List<Models.Message> getMessages(int patientId, int doctorId)
        {
            var result = new List<Models.Message>();

            var connectionString = """
                Server=localhost;
                Database=medsy;
                User=root;
                Password=pqkiet854;
                """;
            var connection = new MySqlConnection(connectionString);
            connection.Open();

            var messageSql = $"""
                SELECT content, senderId, receiverId
                FROM message
                WHERE (senderId=@patientId and receiverId=@doctorId) 
                      or (senderId=@doctorId and receiverId=@patientId)
                """;
            var messageCommand = new MySqlCommand(messageSql, connection);
            messageCommand.Parameters.Add("@patientId", MySqlDbType.Int64)
                .Value = patientId;
            messageCommand.Parameters.Add("doctorId", MySqlDbType.Int64)
                .Value = doctorId;

            var reader = messageCommand.ExecuteReader();
            while(reader.Read())
            {
                var message = new Models.Message();
                message.senderId = (int)reader["senderId"];
                message.receiverId = (int)reader["receiverId"];
                message.content = (string)reader["content"];
                result.Add(message);
            }

            connection.Close();
            return result;
        }
    }
}

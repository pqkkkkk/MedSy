using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;

namespace MedSy.Services.User
{
    public class UserSqlDao : IUserDao
    {
        private List<Models.User> users;
        private SqlConnection connection;
        public UserSqlDao()
        {
            // Khởi tạo danh sách người dùng
            users = new List<Models.User>();
            connection = (Application.Current as App).locator.sqlConnection;
        }

        public List<Models.User> getAllUsers()
        {
    
            connection.Open();

            // truy vấn lấy tất cả người dùng
            var sqlQuery = $"""
                SELECT COUNT(*) OVER() AS TotalCount, ID, Username, FullName, Email, PhoneNumber, Address, Birthday, userRole 
                FROM Users 
                """;

            var command = new SqlCommand(sqlQuery, connection);

            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Lấy thông tin người dùng từ reader và tạo đối tượng User
                    var user = new Models.User
                    {
                        id = reader.GetInt32(reader.GetOrdinal("ID")),
                        username = reader.GetString(reader.GetOrdinal("Username")),
                        fullName = reader.GetString(reader.GetOrdinal("FullName")),
                        email = reader.GetString(reader.GetOrdinal("Email")),
                        phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        address = reader.GetString(reader.GetOrdinal("Address")),
                        birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                        role = reader.GetString(reader.GetOrdinal("userRole"))
                    };
                    users.Add(user); // Thêm vào danh sách
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return users;
        }

        public Models.User getUserByUsername(string username)
        {
            
            connection.Open();
 
            var sqlQuery = $"""
                SELECT *
                FROM Users 
                WHERE Username = @Username;
                """;


            var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Username", username);

            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = new Models.User
                    {
                        id = reader.GetInt32(reader.GetOrdinal("ID")),
                        username = reader.GetString(reader.GetOrdinal("Username")),
                        password = reader.GetString(reader.GetOrdinal("Password")),
                        fullName = reader.GetString(reader.GetOrdinal("FullName")),
                        email = reader.GetString(reader.GetOrdinal("Email")),
                        phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        address = reader.GetString(reader.GetOrdinal("Address")),
                        birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                        role = reader.GetString(reader.GetOrdinal("userRole"))
                    };
                    return user;
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

            return null;

        }
        public Models.User getUserById(int id)
        {
            connection.Open();

            var sqlQuery = $"""
                SELECT *
                FROM Users 
                WHERE ID = @ID;
                """;
            var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var user = new Models.User
                    {
                        id = reader.GetInt32(reader.GetOrdinal("ID")),
                        username = reader.GetString(reader.GetOrdinal("Username")),
                        password = reader.GetString(reader.GetOrdinal("Password")),
                        fullName = reader.GetString(reader.GetOrdinal("FullName")),
                        email = reader.GetString(reader.GetOrdinal("Email")),
                        phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        address = reader.GetString(reader.GetOrdinal("Address")),
                        birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                        role = reader.GetString(reader.GetOrdinal("userRole"))
                    };
                    return user;
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

            return null;
        }
    }
}

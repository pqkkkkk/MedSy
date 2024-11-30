using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MedSy.Services.User
{
    public class UserSqlDao : IUserDao
    {
        private List<Models.User> users;

        public UserSqlDao()
        {
            // Khởi tạo danh sách người dùng
            users = new List<Models.User>();
        }

        public List<Models.User> getAllUsers()
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
            var connectionString = $"""
                Server = localhost;
                Database = medsy;
                User ID = sa;
                Password = SqlServer@123;
                TrustServerCertificate = True;
                """;

            var connection = new SqlConnection(connectionString);
            connection.Open();

            // truy vấn lấy tất cả người dùng
            var sqlQuery = $"""
                SELECT ID, Username, Password, FullName, Email, PhoneNumber, Address, Birthday, userRole 
                FROM Users 
                WHERE Username = @Username;
                """;


            var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Username", username); // Thêm tham số @Username vào câu truy vấn

            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read()) // Nếu có kết quả trả về
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

            return null; // Trả về đối tượng người dùng nếu tìm thấy, nếu không thì null

        }
        public Models.User getUserById(int id)
        {
            return null;
        }
    }
}

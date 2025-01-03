using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
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
                    var user = new Models.User
                    {
                        id = reader.GetInt32(reader.GetOrdinal("ID")),
                        username = reader.GetString(reader.GetOrdinal("Username")),
                        fullName = reader.GetString(reader.GetOrdinal("FullName")),
                        email = reader.GetString(reader.GetOrdinal("Email")),
                        phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        address = reader.GetString(reader.GetOrdinal("Address")),
                        birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                        role = reader.GetString(reader.GetOrdinal("role"))
                    };
                    users.Add(user);
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
                        gender = reader.GetString(reader.GetOrdinal("gender")),
                        birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                        role = reader.GetString(reader.GetOrdinal("role"))
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
                        role = reader.GetString(reader.GetOrdinal("role")),
                        gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? "Unknown" : reader.GetString(reader.GetOrdinal("gender"))
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

        public Tuple<List<Doctor>, int> GetDoctors(int page, int rowsPerPage, string keyword, string specialty, string gender, int experienceYear)
        {
            connection.Open();
            var sql = """
                SELECT *
                FROM users u JOIN doctor_information d ON u.id = d.id
                WHERE u.role = 'doctor'
                """;
            var command = new SqlCommand(sql, connection);
            var doctors = new List<Doctor>();

            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var doctor = new Models.Doctor
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id")),
                        username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "" : reader.GetString(reader.GetOrdinal("Username")),
                        fullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? "" : reader.GetString(reader.GetOrdinal("FullName")),
                        email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                        phoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? "" : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                        birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? new DateTime(0,0,0) : reader.GetDateTime(reader.GetOrdinal("Birthday")),
                        avatarPath = reader.IsDBNull(reader.GetOrdinal("Avatar_Path")) ? "" : reader.GetString(reader.GetOrdinal("Avatar_Path")),
                        gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? "" : reader.GetString(reader.GetOrdinal("gender")),
                        role = reader.IsDBNull(reader.GetOrdinal("role")) ? "" : reader.GetString(reader.GetOrdinal("role")),
                        experienceYear = reader.IsDBNull(reader.GetOrdinal("Experience_Year")) ? 0 : reader.GetInt32(reader.GetOrdinal("Experience_Year")),
                        consultation_price = reader.IsDBNull(reader.GetOrdinal("Consultation_Price")) ? 0 : reader.GetInt32(reader.GetOrdinal("Consultation_Price")),
                        speciality = reader.IsDBNull(reader.GetOrdinal("Speciality")) ? "" : reader.GetString(reader.GetOrdinal("Speciality")),
                        rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0 : (float)reader.GetDouble(reader.GetOrdinal("Rating"))
                    };
                    doctors.Add(doctor);
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
            var query = doctors.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(d => d.fullName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(specialty))
                query = query.Where(d => d.speciality == specialty);

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(d => d.gender == gender);

            if (experienceYear > 0)
                query = query.Where(d => d.experienceYear == experienceYear);

            var result = query.Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToList();
            return new Tuple<List<Doctor>, int>(result, query.Count());
        }
    }
}

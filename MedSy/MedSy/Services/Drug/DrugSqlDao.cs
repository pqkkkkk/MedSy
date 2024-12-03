using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using Microsoft.Data.SqlClient;

namespace MedSy.Services.Drug
{
    public class DrugSqlDao : IDrugDao
    {
        private List<Models.Drug> drugs;
        private List<string> types;
        public List<Models.Drug> getAllDrugs(string keyword, string drugType)
        {
            drugs = new List<Models.Drug>();
            SqlConnection connection = ConnectSql();
            var query = new StringBuilder("SELECT * FROM drug WHERE 1=1");


            if (!string.IsNullOrEmpty(keyword))
            {
                query.Append(" AND name LIKE @keyword");
            }

            if ((drugType != "All"))
            {
                query.Append(" AND drug_type = @drugType");
            }
            connection.Open();
            var command = new SqlCommand(query.ToString(), connection);

            if (!string.IsNullOrEmpty(keyword))
            {
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
            }

            if (!string.IsNullOrEmpty(drugType))
            {
                command.Parameters.AddWithValue("@drugType", drugType);
            }
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var drug = new Models.Drug()
                        {
                            drugId = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            unit = reader.GetString(reader.GetOrdinal("unit")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            manufacturing_date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("manufacturing_date"))),
                            expiry_date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("expiry_date"))),
                            drugTypeName = reader.GetString(reader.GetOrdinal("drug_type"))
                        };
                        drugs.Add(drug);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            connection.Close();
            return drugs;
        }

        public List<string> getAllDrugType()
        {
            types = new List<string>();
            SqlConnection connection = ConnectSql();
            var query = $"""SELECT distinct drug_type FROM drug""";

            var command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        types.Add(reader.GetString(reader.GetOrdinal("drug_type")));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            connection.Close();
            return types;
        }

        public void updateQuantity(int quantity, int id)
        {
            SqlConnection connection = ConnectSql();
            var query = $"""
                UPDATE drug
                SET quantity = @quantity
                WHERE id = @id
                """;

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.Parameters.AddWithValue("@id", id);


            try
            {
                // Mở kết nối
                connection.Open();

                // Thực thi lệnh cập nhật
                int rowsAffected = command.ExecuteNonQuery();

                // Kiểm tra số hàng đã cập nhật
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Cập nhật thành công!");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy bản ghi để cập nhật.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối luôn được đóng
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            try
            {
                // Mở kết nối
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Update successful!");
                }
                else
                {
                    Console.WriteLine("No record found to update.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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

    }
}
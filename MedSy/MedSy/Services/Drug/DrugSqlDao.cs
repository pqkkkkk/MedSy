using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;
using Mysqlx.Resultset;

namespace MedSy.Services.Drug
{
    public class DrugSqlDao : IDrugDao
    {
        private SqlConnection connection;
        private List<Models.Drug> drugs;
        private List<string> types;
        public DrugSqlDao()
        {
            connection = (Application.Current as App).locator.sqlConnection;
        }
        public List<Models.Drug> getAllDrugs(string keyword, string drugType)
        {
            drugs = new List<Models.Drug>();
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
        public Tuple<List<Models.Drug>, int> GetDrugs(int page, int rowsPerPage, string keyword, string drugType, double minPrice, double maxPrice)
        {
            var drugs = new List<Models.Drug>();
            int totalItems = 0;
            connection.Open();

            // Tạo điều kiện truy vấn
            var whereClause = new List<string> { "1=1" };
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                whereClause.Add("name LIKE @keyword");
                parameters.Add(new SqlParameter("@keyword", $"%{keyword}%"));
            }

            if (!string.IsNullOrWhiteSpace(drugType) && drugType != "All")
            {
                whereClause.Add("drug_type = @selectedType");
                parameters.Add(new SqlParameter("@selectedType", drugType));
            }

            if (minPrice > 0)
            {
                whereClause.Add("price >= @minPrice");
                parameters.Add(new SqlParameter("@minPrice", minPrice));
            }

            if (maxPrice > 0)
            {
                whereClause.Add("price <= @maxPrice");
                parameters.Add(new SqlParameter("@maxPrice", maxPrice));
            }

            string whereClauseString = string.Join(" AND ", whereClause);

            // Truy vấn dữ liệu và tổng số bản ghi
            string query = $@"
                WITH CTE_Drugs AS (
                    SELECT *, COUNT(*) OVER() AS TotalRecords
                    FROM drug
                    WHERE {whereClauseString}
                )
                SELECT * 
                FROM CTE_Drugs
                ORDER BY name
                OFFSET @offset ROWS FETCH NEXT @rowsPerPage ROWS ONLY;
            ";

            parameters.Add(new SqlParameter("@offset", (page - 1) * rowsPerPage));
            parameters.Add(new SqlParameter("@rowsPerPage", rowsPerPage));

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters.ToArray());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drugs.Add(new Models.Drug
                        {
                            drugId = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            unit = reader.GetString(reader.GetOrdinal("unit")),
                            price = reader.GetInt32(reader.GetOrdinal("price")),
                            quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            manufacturing_date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("manufacturing_date"))),
                            expiry_date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("expiry_date"))),
                            drugTypeName = reader.GetString(reader.GetOrdinal("drug_type"))
                        });

                        // Lấy tổng số bản ghi từ cột "TotalRecords" (dòng đầu tiên là đủ)
                        if (totalItems == 0)
                        {
                            totalItems = reader.GetInt32(reader.GetOrdinal("TotalRecords"));
                        }
                    }
                }
            }
            connection.Close();
            return new Tuple<List<Models.Drug>, int>(drugs, totalItems);
        }

        public Models.Drug getDrugById(int id)
        {
            var result = new Models.Drug();
            
            var query = $"""SELECT * FROM drug WHERE id = @id""";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            string connectionString = (Application.Current as App).locator.databaseConnectionString;
            try
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var drug = new Models.Drug
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
                        result = drug;
                    }
                }
                
                    return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                connection.Close();
            }

        }

        public bool addDrug(Models.Drug drug)
        {
            connection.Open();
            var query = $"""
                INSERT INTO drug (name, unit, price, quantity, manufacturing_date, expiry_date, drug_type)
                VALUES (@name, @unit, @price, @quantity, @manufacturing_date, @expiry_date, @drug_type)
            """;
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", drug.name);
            command.Parameters.AddWithValue("@unit", drug.unit);
            command.Parameters.AddWithValue("@price", drug.price);
            command.Parameters.AddWithValue("@quantity", drug.quantity);
            command.Parameters.AddWithValue("@manufacturing_date", drug.manufacturing_date);
            command.Parameters.AddWithValue("@expiry_date", drug.expiry_date);
            command.Parameters.AddWithValue("@drug_type", drug.drugTypeName);

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Drug added successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No rows were affected. Please check your input.");
                    return false;
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
            return false;
        }
    }

}
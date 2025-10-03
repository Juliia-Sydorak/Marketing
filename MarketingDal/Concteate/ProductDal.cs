using MarketingDAL.Entities;
using MarketingDAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MarketingDAL.Concrete
{
    public class ProductDal : IProductDal
    {
        private string _connectionString = "Server=localhost;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public Product Create(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Products (ProductName, Price)
                    OUTPUT inserted.ProductID
                    VALUES (@name, @price)";

                command.Parameters.AddWithValue("@name", product.ProductName);
                command.Parameters.AddWithValue("@price", product.Price);

                product.ProductID = (int)command.ExecuteScalar();
            }
            return product;
        }

        public bool Delete(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Products WHERE ProductID = @id";
                command.Parameters.AddWithValue("@id", productId);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Products";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
            }
            return products;
        }

        public Product GetById(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Products WHERE ProductID = @id";
                command.Parameters.AddWithValue("@id", productId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Price = (decimal)reader["Price"]
                        };
                    }
                }
            }
            return null;
        }

        public Product Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Products
                    SET ProductName = @name,
                        Price = @price
                    WHERE ProductID = @id";

                command.Parameters.AddWithValue("@name", product.ProductName);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@id", product.ProductID);

                command.ExecuteNonQuery();
            }
            return product;
        }
    }
}


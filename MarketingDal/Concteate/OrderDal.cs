using MarketingDAL.Entities;
using MarketingDAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MarketingDAL.Concrete
{
    public class OrderDal : IOrderDal
    {
        private string _connectionString = "Server=localhost;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public Order Create(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Orders (UserID, OrderDate)
                    OUTPUT inserted.OrderID
                    VALUES (@userId, @date)";

                command.Parameters.AddWithValue("@userId", order.UserID);
                command.Parameters.AddWithValue("@date", order.OrderDate);

                order.OrderID = (int)command.ExecuteScalar();
            }
            return order;
        }

        public bool Delete(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Orders WHERE OrderID = @id";
                command.Parameters.AddWithValue("@id", orderId);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Order> GetAll()
        {
            var orders = new List<Order>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Orders";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            UserID = (int)reader["UserID"],
                            OrderDate = (DateTime)reader["OrderDate"]
                        });
                    }
                }
            }
            return orders;
        }

        public Order GetById(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Orders WHERE OrderID = @id";
                command.Parameters.AddWithValue("@id", orderId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            UserID = (int)reader["UserID"],
                            OrderDate = (DateTime)reader["OrderDate"]
                        };
                    }
                }
            }
            return null;
        }

        public Order Update(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Orders
                    SET UserID = @userId,
                        OrderDate = @date
                    WHERE OrderID = @id";

                command.Parameters.AddWithValue("@userId", order.UserID);
                command.Parameters.AddWithValue("@date", order.OrderDate);
                command.Parameters.AddWithValue("@id", order.OrderID);

                command.ExecuteNonQuery();
            }
            return order;
        }
    }
}


using MarketingDAL.Entities;
using MarketingDAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MarketingDAL.Concrete
{
    public class OrderItemDal : IOrderItemDal
    {
        private string _connectionString = "Server=localhost;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public OrderItem Create(OrderItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO OrderItems (OrderID, ProductID, Quantity)
                    OUTPUT inserted.OrderItemID
                    VALUES (@orderId, @productId, @quantity)";

                command.Parameters.AddWithValue("@orderId", item.OrderID);
                command.Parameters.AddWithValue("@productId", item.ProductID);
                command.Parameters.AddWithValue("@quantity", item.Quantity);

                item.OrderItemID = (int)command.ExecuteScalar();
            }
            return item;
        }

        public bool Delete(int orderItemId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM OrderItems WHERE OrderItemID = @id";
                command.Parameters.AddWithValue("@id", orderItemId);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<OrderItem> GetAll()
        {
            var items = new List<OrderItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM OrderItems";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new OrderItem
                        {
                            OrderItemID = (int)reader["OrderItemID"],
                            OrderID = (int)reader["OrderID"],
                            ProductID = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"]
                        });
                    }
                }
            }
            return items;
        }

        public OrderItem GetById(int orderItemId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM OrderItems WHERE OrderItemID = @id";
                command.Parameters.AddWithValue("@id", orderItemId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OrderItem
                        {
                            OrderItemID = (int)reader["OrderItemID"],
                            OrderID = (int)reader["OrderID"],
                            ProductID = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"]
                        };
                    }
                }
            }
            return null;
        }

        public OrderItem Update(OrderItem item)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE OrderItems
                    SET OrderID = @orderId,
                        ProductID = @productId,
                        Quantity = @quantity
                    WHERE OrderItemID = @id";

                command.Parameters.AddWithValue("@orderId", item.OrderID);
                command.Parameters.AddWithValue("@productId", item.ProductID);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                command.Parameters.AddWithValue("@id", item.OrderItemID);

                command.ExecuteNonQuery();
            }
            return item;
        }
    }
}


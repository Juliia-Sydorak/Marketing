using MarketingDAL.Entities;
using MarketingDAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MarketingDAL.Concrete
{
    public class DiscountDal : IDiscountDal
    {
        private string _connectionString = "Server=localhost;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public Discount Create(Discount discount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Discounts (UserID, DiscountPercent, SetDate)
                    OUTPUT inserted.DiscountID
                    VALUES (@userId, @percent, @date)";

                command.Parameters.AddWithValue("@userId", discount.UserID);
                command.Parameters.AddWithValue("@percent", discount.DiscountPercent);
                command.Parameters.AddWithValue("@date", discount.SetDate);

                discount.DiscountID = (int)command.ExecuteScalar();
            }
            return discount;
        }

        public bool Delete(int discountId) 
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Discounts WHERE DiscountID = @id";
                command.Parameters.AddWithValue("@id", discountId);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Discount> GetAll()
        {
            var discounts = new List<Discount>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Discounts";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        discounts.Add(new Discount
                        {
                            DiscountID = (int)reader["DiscountID"],
                            UserID = (int)reader["UserID"],
                            DiscountPercent = (decimal)reader["DiscountPercent"],
                            SetDate = (DateTime)reader["SetDate"]
                        });
                    }
                }
            }
            return discounts;
        }

        public Discount GetById(int discountId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Discounts WHERE DiscountID = @id";
                command.Parameters.AddWithValue("@id", discountId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Discount
                        {
                            DiscountID = (int)reader["DiscountID"],
                            UserID = (int)reader["UserID"],
                            DiscountPercent = (decimal)reader["DiscountPercent"],
                            SetDate = (DateTime)reader["SetDate"]
                        };
                    }
                }
            }
            return null;
        }

        public Discount Update(Discount discount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Discounts
                    SET UserID = @userId,
                        DiscountPercent = @percent,
                        SetDate = @date
                    WHERE DiscountID = @id";

                command.Parameters.AddWithValue("@userId", discount.UserID);
                command.Parameters.AddWithValue("@percent", discount.DiscountPercent);
                command.Parameters.AddWithValue("@date", discount.SetDate);
                command.Parameters.AddWithValue("@id", discount.DiscountID);

                command.ExecuteNonQuery();
            }
            return discount;
        }
    }
}


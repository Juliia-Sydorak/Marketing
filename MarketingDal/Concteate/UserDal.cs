using MarketingDAL.Entities;
using MarketingDAL.Interfaces;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MarketingDAL.Concrete
{
    public class UserDal : IUserDal
    {
        private string _connectionString;

        public UserDal()
        {
            _connectionString = "Server=localhost;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public UserDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User Create(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Users (FullName, Email, UserRole, RegistrationDate)
                    OUTPUT inserted.UserID
                    VALUES (@fullName, @email, @userRole, @registrationDate)";

                command.Parameters.AddWithValue("@fullName", user.FullName);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@userRole", user.UserRole);
                command.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);

                user.UserID = (int)command.ExecuteScalar();
            }

            return user;
        }

        public bool Delete(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Users WHERE UserID = @userId";
                command.Parameters.AddWithValue("@userId", userId);

                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserID = (int)reader["UserID"],
                            FullName = (string)reader["FullName"],
                            Email = (string)reader["Email"],
                            UserRole = (string)reader["UserRole"],
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public User GetById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users WHERE UserID = @userId";
                command.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = (int)reader["UserID"],
                            FullName = (string)reader["FullName"],
                            Email = (string)reader["Email"],
                            UserRole = (string)reader["UserRole"],
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                    }
                }
            }

            return null;
        }

        public User Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Users
                    SET FullName = @fullName,
                        Email = @email,
                        UserRole = @userRole,
                        RegistrationDate = @registrationDate
                    WHERE UserID = @userId";

                command.Parameters.AddWithValue("@fullName", user.FullName);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@userRole", user.UserRole);
                command.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);
                command.Parameters.AddWithValue("@userId", user.UserID);

                command.ExecuteNonQuery();
            }

            return user;
        }
    }
}

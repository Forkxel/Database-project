using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
{
    public class DatabaseInteraction
    {
        SqlConnection conn = DatabaseConnection.GetInstance();

        public void CreateTables()
        {
            using (SqlCommand command = new SqlCommand("CREATE TABLE Members (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL, email VARCHAR(50) NOT NULL, membershipDate datetime NOT NULL);", conn))
            {
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand("CREATE TABLE Author (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL);", conn))
            {
                command.ExecuteNonQuery();
            }
            using(SqlCommand command = new SqlCommand("CREATE TABLE Category (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL);", conn))
            {
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand("CREATE TABLE Books (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, title VARCHAR(50) NOT NULL, authorId INT FOREIGN KEY REFERENCES Author(id) NOT NULL, categoryId INT FOREIGN KEY REFERENCES Category(id) NOT NULL, price float NOT NULL, isAvailable bit NOT NULL);", conn))
            {
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand("CREATE TABLE Loans (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, memberId INT FOREIGN KEY REFERENCES Members(id) NOT NULL, bookId INT FOREIGN KEY REFERENCES Books(id) NOT NULL, loanDate datetime NOT NULL, returnDate datetime);", conn))
            {
                command.ExecuteNonQuery();
            }

            DatabaseConnection.CloseConnection();
        }
    }
}

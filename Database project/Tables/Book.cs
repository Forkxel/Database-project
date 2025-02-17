using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    /// <summary>
    /// Class for table Books
    /// </summary>
    public class Book : IMethods<Book>
    {
        private float price;

        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }

        public float Price
        {
            get => price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }

                price = value;
            }
        }

        public bool IsAvailable { get; set; }
        private SqlConnection connection = DatabaseConnection.GetInstance();

        public Book(int id, string title, int authorID, int categoryID, float price, bool isAvailable)
        {
            ID = id;
            Title = title;
            AuthorID = authorID;
            CategoryID = categoryID;
            Price = price;
            IsAvailable = isAvailable;
        }

        public Book() {}

        /// <summary>
        /// Method to insert data to the table books
        /// </summary>
        /// <param name="element">book that is inserted to table</param>
        public void InsertData(Book element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Books (title, authorID, categoryID, price, isAvailable) VALUES (@title, @authorID, @categoryID, @price, @isAvailable);", connection))
            {
                command.Parameters.AddWithValue("@title", element.Title);
                command.Parameters.AddWithValue("@authorID", element.AuthorID);
                command.Parameters.AddWithValue("@categoryID", element.CategoryID);
                command.Parameters.AddWithValue("@price", element.Price);
                command.Parameters.AddWithValue("@isAvailable", element.IsAvailable);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to update data from table books
        /// </summary>
        /// <param name="element">book that is updated</param>
        /// <param name="column">list of columns that are updated</param>
        public void UpdateData(Book element, List<int> column)
        {
            string query = "UPDATE Books SET ";
            for (int i = 0; i < column.Count; i++)
            {
                switch (column[i])
                {
                    case 1: 
                        query += "title = @title"; 
                        break;
                    case 2: 
                        query += "authorID = @authorID"; 
                        break;
                    case 3: 
                        query += "categoryID = @categoryID"; 
                        break;
                    case 4: 
                        query += "price = @price"; 
                        break;
                    case 5: 
                        query += "isAvailable = @isAvailable"; 
                        break;
                }
                if (i < column.Count - 1) query += ", ";
            }
            query += " WHERE ID = @id;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.Parameters.AddWithValue("@title", element.Title);
                command.Parameters.AddWithValue("@authorID", element.AuthorID);
                command.Parameters.AddWithValue("@categoryID", element.CategoryID);
                command.Parameters.AddWithValue("@price", element.Price);
                command.Parameters.AddWithValue("@isAvailable", element.IsAvailable);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to delete data from table books
        /// </summary>
        /// <param name="element">book that is deleted</param>
        public void DeleteData(Book element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Books WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to print all books from database
        /// </summary>
        public void WriteAll()
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Books", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("ID, Title, AuthorID, CategoryID, Price, IsAvailable");
                Console.WriteLine();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetInt32(2)}, {reader.GetInt32(3)}, {reader.GetDouble(4)}, {reader.GetBoolean(5)}");
                }
                reader.Close();
                Console.WriteLine();
            }
        }
    }
}

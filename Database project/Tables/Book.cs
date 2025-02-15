using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Book : Methods<Book>
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

        public Book()
        {
        }

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

        public void UpdateData(Book element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Book element)
        {
            throw new NotImplementedException();
        }
    }
}

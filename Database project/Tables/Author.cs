using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Author : Methods<Author>
    {
        private SqlConnection connection = DatabaseConnection.GetInstance();
        public int ID { get; set; }
        public string Name { get; set; }

        public Author(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Author()
        {
        }

        public void InsertData(Author element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Author(name) VALUES (@name);", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Author element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Author element)
        {
            throw new NotImplementedException();
        }
    }
}

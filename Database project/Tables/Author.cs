using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Author : IMethods<Author>
    {
        private SqlConnection connection = DatabaseConnection.GetInstance();
        public int ID { get; set; }
        public string Name { get; set; }

        public Author(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Author() {}

        public void InsertData(Author element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Author(name) VALUES (@name);", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Author element, List<int> column)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Author SET name = @name WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteData(Author element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Author WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void WriteAll()
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Author;", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("ID, Name");
                Console.WriteLine();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                }
                reader.Close();
                Console.WriteLine();
            }
        }
    }
}

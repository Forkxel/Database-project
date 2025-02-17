using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Database_project.Tables
{
    public class Author : IMethods<Author>
    {
        private SqlConnection connection = DatabaseConnection.GetInstance();
        public int ID { get; set; }
        
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public Author(int id, string firstName, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Author() {}

        public void InsertData(Author element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Author(firstName, lastName) VALUES (@firstName, @lastName);", connection))
            {
                command.Parameters.AddWithValue("@firstName", element.FirstName);
                command.Parameters.AddWithValue("@lastName", element.LastName);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Author element, List<int> column)
        {
            string query = "UPDATE Members SET ";
            for (int i = 0; i < column.Count; i++)
            {
                switch (column[i])
                {
                    case 1: 
                        query += "firstName = @firstName"; 
                        break;
                    case 2: 
                        query += "lastName = @lastName"; 
                        break;
                }
                if (i < column.Count - 1) query += ", ";
            }
            query += " WHERE ID = @id;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@firstName", element.FirstName);
                command.Parameters.AddWithValue("@lastName", element.LastName);
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
                Console.WriteLine("ID, First Name, Last Name");
                Console.WriteLine();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
                }
                reader.Close();
                Console.WriteLine();
            }
        }
    }
}

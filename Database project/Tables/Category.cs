﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Category : IMethods<Category>
    {
        public int ID { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        private SqlConnection connection = DatabaseConnection.GetInstance();

        public Category(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Category() {}

        public void InsertData(Category element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Category(name) VALUES (@name);", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Category element, List<int> column)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Category SET name = @name WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteData(Category element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Category WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void WriteAll()
        {
            using (SqlCommand command = new SqlCommand("Select * FROM Category;", connection))
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

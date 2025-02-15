﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Category : Methods<Category>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private SqlConnection connection = DatabaseConnection.GetInstance();

        public Category(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Category()
        {
        }

        public void InsertData(Category element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Category(name) VALUES (@name);", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Category element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Category element)
        {
            throw new NotImplementedException();
        }
    }
}

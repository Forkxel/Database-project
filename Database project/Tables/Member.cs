using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Member : IMethods<Member>
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private SqlConnection connection = DatabaseConnection.GetInstance();

        public Member(int id, string firstName, string email, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Member() {}

        public void InsertData(Member element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Members (firstName ,lastName ,email) VALUES (@firstName ,@lastName , @email);", connection))
            {
                command.Parameters.AddWithValue("@firstName", element.FirstName);
                command.Parameters.AddWithValue("@lastName", element.LastName);
                command.Parameters.AddWithValue("@email", element.Email);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Member element, List<int> column)
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
                    case 3: 
                        query += "email = @email"; 
                        break;
                }
                if (i < column.Count - 1) query += ", ";
            }
            query += " WHERE ID = @id;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@firstName", element.FirstName);
                command.Parameters.AddWithValue("@lastName", element.LastName);
                command.Parameters.AddWithValue("@email", element.Email);
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteData(Member element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Members WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void WriteAll()
        {
            using (SqlCommand command = new SqlCommand("Select * FROM Members;", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("ID, First Name, Last Name, Email");
                Console.WriteLine();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetString(3)}");
                }
                reader.Close();
                Console.WriteLine();
            }
        }
    }
}

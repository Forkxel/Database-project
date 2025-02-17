using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    /// <summary>
    /// Class for table Members
    /// </summary>
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

        /// <summary>
        /// Method to insert data to the table members
        /// </summary>
        /// <param name="element">member that is inserted to table</param>
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

        /// <summary>
        /// Method to update data from table members
        /// </summary>
        /// <param name="element">member that is updated</param>
        /// <param name="column">list of columns that are updated</param>
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

        /// <summary>
        /// Method to delete data from table member
        /// </summary>
        /// <param name="element">member that is deleted</param>
        public void DeleteData(Member element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Members WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method to print all members from database
        /// </summary>
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

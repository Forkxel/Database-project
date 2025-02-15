using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Member : Methods<Member>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MembershipDate { get; set; }

        private SqlConnection connection = DatabaseConnection.GetInstance();

        public Member(int id, string name, string email, string membershipDate)
        {
            ID = id;
            Name = name;
            Email = email;
            MembershipDate = membershipDate;
        }

        public Member()
        {
        }

        public void InsertData(Member element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Members (name, email, membershipDate) VALUES (@name, @email, @date);", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.Parameters.AddWithValue("@email", element.Email);
                command.Parameters.AddWithValue("@date", element.MembershipDate);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Member element)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Members SET name = @name, email = @email, membershipDate = @date WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@name", element.Name);
                command.Parameters.AddWithValue("@email", element.Email);
                command.Parameters.AddWithValue("@date", element.MembershipDate);
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
    }
}

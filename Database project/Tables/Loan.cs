using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Loan : IMethods<Loan>
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public string LoanDate { get; set; }
        public string ReturnDate { get; set; }
        private SqlConnection connection = DatabaseConnection.GetInstance();

        public Loan(int id, int memberID, int bookID, string loanDate, string returnDate)
        {
            ID = id;
            MemberID = memberID;
            BookID = bookID;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }

        public Loan() {}   

        public void InsertData(Loan element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Loans (memberID, bookID, loanDate, returnDate) VALUES (@memberID, @bookID, @loanDate, @returnDate);", connection))
            {
                command.Parameters.AddWithValue("@memberID", element.MemberID);
                command.Parameters.AddWithValue("@bookID", element.BookID);
                command.Parameters.AddWithValue("@loanDate", element.LoanDate);
                command.Parameters.AddWithValue("@returnDate", element.ReturnDate);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Loan element, List<int> column)
        {
            string query = "UPDATE Loans SET ";
            for (int i = 0; i < column.Count; i++)
            {
                switch (column[i])
                {
                    case 1: 
                        query += "memberID = @memberID"; 
                        break;
                    case 2: 
                        query += "bookID = @bookID"; 
                        break;
                    case 3: 
                        query += "loanDate = @loanDate"; 
                        break;
                    case 4: 
                        query += "returnDate = @returnDate"; 
                        break;
                }
                if (i < column.Count - 1) query += ", ";
            }
            query += " WHERE ID = @id;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.Parameters.AddWithValue("@memberID", element.MemberID);
                command.Parameters.AddWithValue("@bookID", element.BookID);
                command.Parameters.AddWithValue("@loanDate", element.LoanDate);
                command.Parameters.AddWithValue("@returnDate", element.ReturnDate);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteData(Loan element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Loans WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void WriteAll()
        {
            using (SqlCommand command = new SqlCommand("Select * FROM Loans;", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("ID, MemberID, BookID, Loan Date, Return Date");
                Console.WriteLine();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetInt32(1)}, {reader.GetInt32(2)}, {reader.GetDateTime(3).ToString("yyyy-MM-dd")}, {reader.GetDateTime(4).ToString("yyyy-MM-dd")}");
                }
                reader.Close();
                Console.WriteLine();
            }
        }
    }
}

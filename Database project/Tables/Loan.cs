using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project.Tables
{
    public class Loan : Methods<Loan>
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

        public Loan()
        {
        }   

        public void InsertData(Loan element)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Loan (memberID, bookID, loanDate, returnDate) VALUES (@memberID, @bookID, @loanDate, @returnDate);", connection))
            {
                command.Parameters.AddWithValue("@memberID", element.MemberID);
                command.Parameters.AddWithValue("@bookID", element.BookID);
                command.Parameters.AddWithValue("@loanDate", element.LoanDate);
                command.Parameters.AddWithValue("@returnDate", element.ReturnDate);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateData(Loan element)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Loan SET memberID = @memberID, bookID = @bookID, loanDate = @loanDate, returnDate = @returnDate WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@memberID", element.MemberID);
                command.Parameters.AddWithValue("@bookID", element.BookID);
                command.Parameters.AddWithValue("@loanDate", element.LoanDate);
                command.Parameters.AddWithValue("@returnDate", element.ReturnDate);
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteData(Loan element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Loan WHERE ID = @id;", connection))
            {
                command.Parameters.AddWithValue("@id", element.ID);
                command.ExecuteNonQuery();
            }
        }
    }
}

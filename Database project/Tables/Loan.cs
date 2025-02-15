using System;
using System.Collections.Generic;
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
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Loan(int id, int memberID, int bookID, DateTime loanDate, DateTime returnDate)
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
            throw new NotImplementedException();
        }

        public void UpdateData(Loan element)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(Loan element)
        {
            throw new NotImplementedException();
        }
    }
}

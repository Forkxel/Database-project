using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_project
{
    public class Loan
    {
        private int id;
        private int memberID;
        private int bookID;
        private DateTime loanDate;
        private DateTime returnDate;

        public int ID { get => id; set => id = value; }
        public int MemberID { get => memberID; set => memberID = value; }
        public int BookID { get => bookID; set => bookID = value; }
        public DateTime LoanDate { get => loanDate; set => loanDate = value; }
        public DateTime ReturnDate { get => returnDate; set => returnDate = value; }

        public Loan(int id, int memberID, int bookID, DateTime loanDate, DateTime returnDate)
        {
            this.ID = id;
            this.MemberID = memberID;
            this.BookID = bookID;
            this.LoanDate = loanDate;
            this.ReturnDate = returnDate;
        }
    }
}

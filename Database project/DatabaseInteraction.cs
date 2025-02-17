using Database_project.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database_project
{
    public class DatabaseInteraction
    {
        private SqlConnection connection = DatabaseConnection.GetInstance();
        private Regex regex = new("^(\\d{4})-(0[1-9]|1[0-2]|[1-9])-([1-9]|0[1-9]|[1-2]\\d|3[0-1])$");
        private Book book = new();
        private Author author = new();
        private Category category = new();
        private Loan loan = new();
        private Member member = new();

        public void CreateTables()
        {
            try
            {
                using (SqlCommand command = new SqlCommand("CREATE TABLE Members (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, firstName VARCHAR(20) NOT NULL, lastName VARCHAR(20) NOT NULL , email VARCHAR(50) NOT NULL);", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e) {}

            try
            {
                using (SqlCommand command = new SqlCommand("CREATE TABLE Author (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, firstName VARCHAR(20) NOT NULL, lastName VARCHAR(20) NOT NULL);", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e) {}

            try
            {
                using(SqlCommand command = new SqlCommand("CREATE TABLE Category (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL);", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e) {}

            try
            {
                using (SqlCommand command = new SqlCommand(
                           "CREATE TABLE Books (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, title VARCHAR(50) NOT NULL, authorId INT FOREIGN KEY REFERENCES Author(id) ON DELETE CASCADE, categoryId INT FOREIGN KEY REFERENCES Category(id) ON DELETE CASCADE, price float NOT NULL, isAvailable bit NOT NULL);",
                           connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                using (SqlCommand command = new SqlCommand(
                           "CREATE TABLE Loans (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, memberId INT FOREIGN KEY REFERENCES Members(id) ON DELETE CASCADE, bookId INT FOREIGN KEY REFERENCES Books(id) ON DELETE CASCADE, loanDate datetime NOT NULL, returnDate datetime);",
                           connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void InsertData(int table)
        {
            try
            {
                switch (table)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine("Enter member first name: ");
                        string memberName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter member last name: ");
                        string memberLastName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter member e-mail: ");
                        string memberEmail = Console.ReadLine();
                        Console.WriteLine();
                        member.InsertData(new Member(0, memberName, memberEmail, memberLastName));
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.WriteLine("Enter book Title: ");
                        string bookTitle = Console.ReadLine();
                        Console.WriteLine();
                        author.WriteAll();
                        Console.WriteLine("Enter author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        category.WriteAll();
                        Console.WriteLine("Enter category ID: ");
                        int categoryID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter price: ");
                        float price = float.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Is available (true/false): ");
                        bool isAvailable = bool.Parse(Console.ReadLine());
                        Console.WriteLine();
                        book.InsertData(new Book(0, bookTitle, authorID, categoryID, price, isAvailable));
                        break;
                    case 3:
                        Console.WriteLine();
                        member.WriteAll();
                        Console.WriteLine("Enter member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        book.WriteAll();
                        Console.WriteLine("Enter book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter loan date (yyyy-mm-dd): ");
                        string loanDate = Console.ReadLine();
                        Console.WriteLine();
                        if (!regex.IsMatch(loanDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine("Enter return date (yyyy-mm-dd): ");
                        Console.WriteLine();
                        string returnDate = Console.ReadLine();
                        if (!regex.IsMatch(returnDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            Console.WriteLine();
                            break;
                        }
                        loan.InsertData(new Loan(0, memberID, bookID, loanDate, returnDate));
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine("Enter author first name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter author last name: ");
                        string authorLastName = Console.ReadLine();
                        Console.WriteLine();
                        author.InsertData(new Author(0, authorName, authorLastName));
                        break;
                    case 5:
                        Console.WriteLine();
                        Console.WriteLine("Enter category Name: ");
                        string categoryName = Console.ReadLine();
                        Console.WriteLine();
                        category.InsertData(new Category(0, categoryName));
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteData(int table)
        {
            try
            {
                switch (table)
                {
                    case 1:
                        Console.WriteLine();
                        member.WriteAll();
                        Console.WriteLine("Enter member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        member.DeleteData(new Member(memberID, "", "", ""));
                        break;
                    case 2:
                        Console.WriteLine();
                        book.WriteAll();
                        Console.WriteLine("Enter book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        book.DeleteData(new Book(bookID, "", 0, 0, 0, false));
                        break;
                    case 3:
                        Console.WriteLine();
                        loan.WriteAll();
                        Console.WriteLine("Enter loan ID: ");
                        int loanID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        loan.DeleteData(new Loan(loanID, 0, 0, "", ""));
                        break;
                    case 4:
                        Console.WriteLine();
                        author.WriteAll();
                        Console.WriteLine("Enter author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        author.DeleteData(new Author(authorID, "", ""));
                        break;
                    case 5:
                        Console.WriteLine();
                        category.WriteAll();
                        Console.WriteLine("Enter category ID: ");
                        int categoryID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        category.DeleteData(new Category(categoryID, ""));
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateData(int table)
        {
            try
            {
                List<int> column = new List<int>();
                string memberName = "";
                string memberLastName = "";
                string memberEmail = "";
                string bookTitle = "";
                int bookAuthorID = 0;
                int bookCategoryID = 0;
                float price = 0;
                bool isAvailable = false;
                int loanMemberID = 0;
                int loanBookID = 0;
                string loanDate = "";
                string returnDate = "";
                string authorName = "";
                string authorLastName = "";
                switch (table)
                {
                    case 1:
                        Console.WriteLine();
                        member.WriteAll();
                        Console.WriteLine("Enter member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Members WHERE ID = @id;", connection))
                        {
                            command.Parameters.AddWithValue("@id", memberID);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                memberName = reader.GetString(1);
                                memberLastName = reader.GetString(2);
                                memberEmail = reader.GetString(3);
                            }
                            reader.Close();
                        }
                        Console.WriteLine("How many columns do you want to update?");
                        int count = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("What columns do you want to update. \n1. First Name \n2. Last Name \n3. E-mail");
                        for (int i = 0; i < count; i++)
                        {
                            int col = int.Parse(Console.ReadLine());
                            if (col < 1 || col > 3)
                            {
                                Console.WriteLine("Invalid column.");
                                i--;
                            }
                            else if (column.Contains(col))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Table already selected.");
                                i--;
                            }
                            else
                            {
                                column.Add(col);
                            }
                        }
                        Console.WriteLine();
                        for (int j = 0; j < column.Count; j++)
                        {
                            switch (column[j])
                            {
                                case 1:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter member first name: ");
                                    memberName = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter member last name: ");
                                    memberLastName = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter member email: ");
                                    memberEmail = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        member.UpdateData(new Member(memberID, memberName, memberEmail, memberLastName), column);
                        break;
                    case 2:
                        Console.WriteLine();
                        book.WriteAll();
                        Console.WriteLine("Enter book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Books WHERE ID = @id;", connection))
                        {
                            command.Parameters.AddWithValue("@id", bookID);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                bookTitle = reader.GetString(1);
                                bookAuthorID = reader.GetInt32(2);
                                bookCategoryID = reader.GetInt32(3);
                                price = (float)reader.GetDouble(4);
                                isAvailable = reader.GetBoolean(5);
                            }
                            reader.Close();
                        }
                        Console.WriteLine("How many columns do you want to update?");
                        int a = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("What columns do you want to update. \n1. Title \n2. Author ID\n3. Category ID\n4. Price\n5. Is Available");
                        for (int i = 0; i < a; i++)
                        {
                            int col = int.Parse(Console.ReadLine());
                            if (col < 1 || col > 5)
                            {
                                Console.WriteLine("Invalid column.");
                                i--;
                            }
                            else if (column.Contains(col))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Table already selected.");
                                i--;
                            }
                            else
                            {
                                column.Add(col);
                            }
                        }
                        Console.WriteLine();
                        for (int j = 0; j < column.Count; j++)
                        {
                            switch (column[j])
                            {
                                case 1:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter book Title: ");
                                    bookTitle = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    author.WriteAll();
                                    Console.WriteLine("Enter author ID: ");
                                    bookAuthorID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    category.WriteAll();
                                    Console.WriteLine("Enter category ID: ");
                                    bookCategoryID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 4:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter price: ");
                                    price = float.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 5:
                                    Console.WriteLine();
                                    Console.WriteLine("Is available (true/false): ");
                                    isAvailable = bool.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        book.UpdateData(new Book(bookID, bookTitle, bookAuthorID, bookCategoryID, price, isAvailable), column);
                        break;
                    case 3:
                        Console.WriteLine();
                        loan.WriteAll();
                        Console.WriteLine("Enter loan ID: ");
                        int loanID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("SELECT * FROM Loans WHERE ID = @id;", connection))
                        {
                            command.Parameters.AddWithValue("@id", loanID);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                loanMemberID = reader.GetInt32(1);
                                loanBookID = reader.GetInt32(2);
                                loanDate = reader.GetDateTime(3).ToString("yyyy-mm-dd");
                                returnDate = reader.GetDateTime(4).ToString("yyyy-mm-dd");
                            }
                            reader.Close();
                        }
                        Console.WriteLine("How many columns do you want to update?");
                        int b = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("What columns do you want to update. \n1. Member ID \n2. Book ID\n3. Loan Date\n4. Return Date");
                        for (int i = 0; i < b; i++)
                        {
                            int col = int.Parse(Console.ReadLine());
                            if (col < 1 || col > 4)
                            {
                                Console.WriteLine("Invalid column.");
                                i--;
                            }
                            else if (column.Contains(col))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Table already selected.");
                                i--;
                            }
                            else
                            {
                                column.Add(col);
                            }
                        }
                        Console.WriteLine();
                        for (int j = 0; j < column.Count; j++)
                        {
                            switch (column[j])
                            {
                                case 1:
                                    Console.WriteLine();
                                    member.WriteAll();
                                    Console.WriteLine("Enter member ID: ");
                                    loanMemberID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    book.WriteAll();
                                    Console.WriteLine("Enter book ID: ");
                                    loanBookID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter loan date (yyyy-MM-dd): ");
                                    loanDate = Console.ReadLine();
                                    Console.WriteLine();
                                    if (!regex.IsMatch(loanDate))
                                    {
                                        Console.WriteLine("Invalid date format.");
                                        Console.WriteLine();
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter return date (yyyy-MM-dd): ");
                                    returnDate = Console.ReadLine();
                                    Console.WriteLine();
                                    if (!regex.IsMatch(returnDate))
                                    {
                                        Console.WriteLine("Invalid date format.");
                                        Console.WriteLine();
                                    }
                                    break;
                            }
                        }
                        loan.UpdateData(new Loan(loanID, loanMemberID, loanBookID, loanDate, returnDate), column);
                        break;
                    case 4:
                        Console.WriteLine();
                        author.WriteAll();
                        Console.WriteLine("Enter author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine("How many columns do you want to update?");
                        int c = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("What columns do you want to update? \n1. First Name \n2. Last Name");
                        for (int i = 0; i < c; i++)
                        {
                            int col = int.Parse(Console.ReadLine());
                            if (col < 1 || col > 2)
                            {
                                Console.WriteLine("Invalid column.");
                                i--;
                            }
                            else if (column.Contains(col))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Table already selected.");
                                i--;
                            }
                            else
                            {
                                column.Add(col);
                            }
                        }
                        Console.WriteLine();
                        for (int j = 0; j < column.Count; j++)
                        {
                            switch (column[j])
                            {
                                case 1:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter author's first name: ");
                                    authorName = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter author's last name: ");
                                    authorLastName = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        author.UpdateData(new Author(authorID, authorName, authorLastName), null);
                        break;
                    case 5:
                        Console.WriteLine();
                        category.WriteAll();
                        Console.WriteLine("Enter category ID: ");
                        int categoryID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter new category name: ");
                        string categoryName = Console.ReadLine();
                        Console.WriteLine();
                        category.UpdateData(new Category(categoryID, categoryName), null);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

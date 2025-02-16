using Database_project.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database_project
{
    public class DatabaseInteraction
    {
        private SqlConnection connection = DatabaseConnection.GetInstance();
        private Regex regex = new Regex("^(\\d{4})-(0[1-9]|1[0-2]|[1-9])-([1-9]|0[1-9]|[1-2]\\d|3[0-1])$");

        public void CreateTables()
        {
            using (SqlCommand command = new SqlCommand("CREATE TABLE Members (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL, email VARCHAR(50) NOT NULL, membershipDate datetime NOT NULL);", connection))
            {
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand("CREATE TABLE Author (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL);", connection))
            {
                command.ExecuteNonQuery();
            }
            using(SqlCommand command = new SqlCommand("CREATE TABLE Category (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, name VARCHAR(20) NOT NULL);", connection))
            {
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand("CREATE TABLE Books (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, title VARCHAR(50) NOT NULL, authorId INT FOREIGN KEY REFERENCES Author(id) NOT NULL, categoryId INT FOREIGN KEY REFERENCES Category(id) NOT NULL, price float NOT NULL, isAvailable bit NOT NULL);", connection))
            {
                command.ExecuteNonQuery();
            }
            using (SqlCommand command = new SqlCommand("CREATE TABLE Loans (id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, memberId INT FOREIGN KEY REFERENCES Members(id) NOT NULL, bookId INT FOREIGN KEY REFERENCES Books(id) NOT NULL, loanDate datetime NOT NULL, returnDate datetime);", connection))
            {
                command.ExecuteNonQuery();
            }

            DatabaseConnection.CloseConnection();
        }

        public void InsertData(int table)
        {
            try
            {

                switch (table)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine("Enter Member Name: ");
                        string memberName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter Member Email: ");
                        string memberEmail = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter Membership Date (yyyy-MM-dd): ");
                        string membershipDate = Console.ReadLine();
                        Console.WriteLine();
                        if (!regex.IsMatch(membershipDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            break;
                        }
                        else
                        {
                            Member member = new Member();
                            member.InsertData(new Member(0, memberName, memberEmail, membershipDate));
                        }
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.WriteLine("Enter Book Title: ");
                        string bookTitle = Console.ReadLine();
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Author;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Category;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Category ID: ");
                        int categoryID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter Price: ");
                        float price = float.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Is Available (true/false): ");
                        bool isAvailable = bool.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Book book = new Book();
                        book.InsertData(new Book(0, bookTitle, authorID, categoryID, price, isAvailable));
                        break;
                    case 3:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Members;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetDateTime(3).ToString("yyyy-MM-dd")}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Books;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetInt32(2)}, {reader.GetInt32(3)}, {reader.GetDouble(4)}, {reader.GetBoolean(5)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter Loan Date (yyyy-MM-dd): ");
                        string loanDate = Console.ReadLine();
                        Console.WriteLine();
                        if (!regex.IsMatch(loanDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine("Enter Return Date (yyyy-MM-dd): ");
                        Console.WriteLine();
                        string returnDate = Console.ReadLine();
                        if (!regex.IsMatch(returnDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            Console.WriteLine();
                            break;
                        }
                        Loan loan = new Loan();
                        loan.InsertData(new Loan(0, memberID, bookID, loanDate, returnDate));
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine("Enter Author Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine();
                        Author author = new Author();
                        author.InsertData(new Author(0, authorName));
                        break;
                    case 5:
                        Console.WriteLine();
                        Console.WriteLine("Enter Category Name: ");
                        string categoryName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Category category = new Category();
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
                        using (SqlCommand command = new SqlCommand("Select * FROM Members;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetString(3)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Member member = new Member();
                        member.DeleteData(new Member(memberID, "", "", ""));
                        break;
                    case 2:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Books;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetInt32(2)}, {reader.GetInt32(3)}, {reader.GetDouble(4)}, {reader.GetBoolean(5)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Book book = new Book();
                        book.DeleteData(new Book(bookID, "", 0, 0, 0, false));
                        break;
                    case 3:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Loans;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetInt32(1)}, {reader.GetInt32(2)}, {reader.GetString(3)}, {reader.GetString(4)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Loan ID: ");
                        int loanID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Loan loan = new Loan();
                        loan.DeleteData(new Loan(loanID, 0, 0, "", ""));
                        break;
                    case 4:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Author;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Author author = new Author();
                        author.DeleteData(new Author(authorID, ""));
                        break;
                    case 5:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Category;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                            Console.WriteLine("Enter Category ID: ");
                            int categoryID = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            Category category = new Category();
                            category.DeleteData(new Category(categoryID, ""));
                            break;
                        }
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
                string memberEmail = "";
                string membershipDate = "";
                string bookTitle = "";
                int bookAuthorID = 0;
                int bookCategoryID = 0;
                float price = 0;
                bool isAvailable = false;
                int loanMemberID = 0;
                int loanBookID = 0;
                string loanDate = "";
                string returnDate = "";
                switch (table)
                {
                    case 1:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Members;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetDateTime(3).ToString("yyyy-mm-dd")}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Members WHERE ID = @id;", connection))
                        {
                            command.Parameters.AddWithValue("@id", memberID);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                memberName = reader.GetString(1);
                                memberEmail = reader.GetString(2);
                                membershipDate = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("How many columns do you want to update?");
                        int count = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("What columns do you want to update. \n1. Name \n2. E-mail\n3. Membership date");
                        for (int i = 0; i < count; i++)
                        {
                            int col = int.Parse(Console.ReadLine());
                            if (column.Contains(col))
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
                                    Console.WriteLine("Enter Member Name: ");
                                    memberName = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter Member Email: ");
                                    memberEmail = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter Membership Date (yyyy-MM-dd): ");
                                    membershipDate = Console.ReadLine();
                                    Console.WriteLine();
                                    if (!regex.IsMatch(membershipDate))
                                    {
                                        Console.WriteLine("Invalid date format.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    break;
                            }
                        }
                        Member member = new Member();
                        member.UpdateData(new Member(memberID, memberName, memberEmail, membershipDate), column);
                        break;
                    case 2:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Books;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetInt32(2)}, {reader.GetInt32(3)}, {reader.GetDouble(4)}, {reader.GetBoolean(5)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Books WHERE ID = @id;", connection))
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
                            if (column.Contains(col))
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
                                    Console.WriteLine("Enter Book Title: ");
                                    bookTitle = Console.ReadLine();
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    using (SqlCommand command = new SqlCommand("Select * FROM Author;", connection))
                                    {
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                                        }
                                        reader.Close();
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Enter Author ID: ");
                                    bookAuthorID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    using (SqlCommand command = new SqlCommand("Select * FROM Category;", connection))
                                    {
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                                        }
                                        reader.Close();
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Enter Category ID: ");
                                    bookCategoryID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 4:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter Price: ");
                                    price = float.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 5:
                                    Console.WriteLine();
                                    Console.WriteLine("Is Available (true/false): ");
                                    isAvailable = bool.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                            }
                        }
                        Book book = new Book();
                        book.UpdateData(new Book(bookID, bookTitle, bookAuthorID, bookCategoryID, price, isAvailable), column);
                        break;
                    case 3:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Loans;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetInt32(1)}, {reader.GetInt32(2)}, {reader.GetDateTime(3).ToString("yyyy-MM-dd")}, {reader.GetDateTime(4).ToString("yyyy-MM-dd")}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Loan ID: ");
                        int loanID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Loans WHERE ID = @id;", connection))
                        {
                            command.Parameters.AddWithValue("@id", loanID);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                loanMemberID = reader.GetInt32(1);
                                loanBookID = reader.GetInt32(2);
                                loanDate = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                                returnDate = reader.GetDateTime(4).ToString("yyyy-MM-dd");
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
                            if (column.Contains(col))
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
                                    using (SqlCommand command = new SqlCommand("Select * FROM Members;", connection))
                                    {
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetDateTime(3).ToString("yyyy-MM-dd")}");
                                        }
                                        reader.Close();
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Enter Member ID: ");
                                    loanMemberID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 2:
                                    Console.WriteLine();
                                    using (SqlCommand command = new SqlCommand("Select * FROM Books;", connection))
                                    {
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetInt32(2)}, {reader.GetInt32(3)}, {reader.GetDouble(4)}, {reader.GetBoolean(5)}");
                                        }
                                        reader.Close();
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("Enter Book ID: ");
                                    loanBookID = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter Loan Date (yyyy-MM-dd): ");
                                    loanDate = Console.ReadLine();
                                    Console.WriteLine();
                                    if (!regex.IsMatch(loanDate))
                                    {
                                        Console.WriteLine("Invalid date format.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine();
                                    Console.WriteLine("Enter Return Date (yyyy-MM-dd): ");
                                    returnDate = Console.ReadLine();
                                    Console.WriteLine();
                                    if (!regex.IsMatch(returnDate))
                                    {
                                        Console.WriteLine("Invalid date format.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    break;
                            }
                        }
                        Loan loan = new Loan();
                        loan.UpdateData(new Loan(loanID, loanMemberID, loanBookID, loanDate, returnDate), column);
                        break;
                    case 4:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Author;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter New Author Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine();
                        Author author = new Author();
                        author.UpdateData(new Author(authorID, authorName), null);
                        break;
                    case 5:
                        Console.WriteLine();
                        using (SqlCommand command = new SqlCommand("Select * FROM Category;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine();
                        }
                        Console.WriteLine("Enter Category ID: ");
                        int categoryID = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Enter New Category Name: ");
                        string categoryName = Console.ReadLine();
                        Console.WriteLine();
                        Category category = new Category();
                        category.UpdateData(new Category(categoryID, categoryName), null);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        public void ImportJSON(string filePath, string jsonProperty, string table)
        {
            var listJSON = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(filePath));
            var list = listJSON[jsonProperty];
            foreach (var item in list)
            {
                using (SqlCommand command = new SqlCommand($"INSERT INTO {table}(name) VALUES (@name);", connection))
                {
                    command.Parameters.AddWithValue("@name", item);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

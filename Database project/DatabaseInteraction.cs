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
                        Console.WriteLine("Enter Member Name: ");
                        string memberName = Console.ReadLine();
                        Console.WriteLine("Enter Member Email: ");
                        string memberEmail = Console.ReadLine();
                        Console.WriteLine("Enter Membership Date (yyyy-MM-dd): ");
                        string membershipDate = Console.ReadLine();
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
                        Console.WriteLine("Enter Book Title: ");
                        string bookTitle = Console.ReadLine();
                        Console.WriteLine("Enter Author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Category ID: ");
                        int categoryID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Price: ");
                        float price = float.Parse(Console.ReadLine());
                        Console.WriteLine("Is Available (true/false): ");
                        bool isAvailable = bool.Parse(Console.ReadLine());
                        Book book = new Book();
                        book.InsertData(new Book(0, bookTitle, authorID, categoryID, price, isAvailable));
                        break;
                    case 3:
                        Console.WriteLine("Enter Member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Loan Date (yyyy-MM-dd): ");
                        string loanDate = Console.ReadLine();
                        if (!regex.IsMatch(loanDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            break;
                        }
                        Console.WriteLine("Enter Return Date (yyyy-MM-dd): ");
                        string returnDate = Console.ReadLine();
                        if (!regex.IsMatch(returnDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            break;
                        }
                        Loan loan = new Loan();
                        loan.InsertData(new Loan(0, memberID, bookID, loanDate, returnDate));
                        break;
                    case 4:
                        Console.WriteLine("Enter Author Name: ");
                        string authorName = Console.ReadLine();
                        Author author = new Author();
                        author.InsertData(new Author(0, authorName));
                        break;
                    case 5:
                        Console.WriteLine("Enter Category Name: ");
                        string categoryName = Console.ReadLine();
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
                        using (SqlCommand command = new SqlCommand("Select * FROM Members;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}, {reader.GetString(3)}");
                            }
                            reader.Close();
                        }
                        Console.WriteLine("Enter Member ID: ");
                        int memberID = int.Parse(Console.ReadLine());
                        Member member = new Member();
                        member.DeleteData(new Member(memberID, "", "", ""));
                        break;
                    case 2:
                        using (SqlCommand command = new SqlCommand("Select * FROM Books;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetInt32(2)}, {reader.GetInt32(3)}, {reader.GetDouble(4)}, {reader.GetBoolean(5)}");
                            }
                            reader.Close();
                        }
                        Console.WriteLine("Enter Book ID: ");
                        int bookID = int.Parse(Console.ReadLine());
                        Book book = new Book();
                        book.DeleteData(new Book(bookID, "", 0, 0, 0, false));
                        break;
                    case 3:
                        using (SqlCommand command = new SqlCommand("Select * FROM Loan;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetInt32(1)}, {reader.GetInt32(2)}, {reader.GetString(3)}, {reader.GetString(4)}");
                            }
                            reader.Close();
                        }
                        Console.WriteLine("Enter Loan ID: ");
                        int loanID = int.Parse(Console.ReadLine());
                        Loan loan = new Loan();
                        loan.DeleteData(new Loan(loanID, 0, 0, "", ""));
                        break;
                    case 4:
                        using (SqlCommand command = new SqlCommand("Select * FROM Author;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                        }
                        Console.WriteLine("Enter Author ID: ");
                        int authorID = int.Parse(Console.ReadLine());
                        Author author = new Author();
                        author.DeleteData(new Author(authorID, ""));
                        break;
                    case 5:
                        using (SqlCommand command = new SqlCommand("Select * FROM Category;", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
                            }
                            reader.Close();
                            Console.WriteLine("Enter Category ID: ");
                            int categoryID = int.Parse(Console.ReadLine());
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

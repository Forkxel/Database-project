namespace Database_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseInteraction db = new DatabaseInteraction();

            //db.ImportJSON("author.json", "name", "Author");
            //db.ImportJSON("categories.json", "categories", "Category");

            //db.CreateTables();

            Console.WriteLine("Do you want to insert, delete or update data?");
            string action = Console.ReadLine();
            if (action == "insert")
            {
                Console.WriteLine("Do you want to insert data into multiple tables? (y/n)");
                string answer = Console.ReadLine();
                List<int> tables = new List<int>();
                Console.WriteLine();

                if (answer == "y")
                {
                    Console.WriteLine("How many tables 1-5");
                    int count = int.Parse(Console.ReadLine());
                    if (count < 1 || count > 5)
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Write a number of table you want to insert into.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                            int table = int.Parse(Console.ReadLine());
                            if (tables.Contains(table))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Table already selected.");
                                i--;
                            }
                            else
                            {
                                tables.Add(table);
                            }
                        }
                    }
                }
                else if (answer == "n")
                {
                    Console.WriteLine("Write a number of table you want to insert into.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                    int table = int.Parse(Console.ReadLine());
                    if (table < 1 || table > 5)
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    else
                    {
                        db.InsertData(table);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
       
            else if (action == "delete")
            {
                Console.WriteLine("Write a number of table you want to delete from.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                int table = int.Parse(Console.ReadLine());
                if (table < 1 || table > 5)
                {
                    Console.WriteLine("Invalid input.");
                }
                else
                {
                    db.DeleteData(table);
                }
            }
            else if (action == "update")
            {
                Console.WriteLine("Write a number of table you want to update.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                int table = int.Parse(Console.ReadLine());
                if (table < 1 || table > 5)
                {
                    Console.WriteLine("Invalid input.");
                }
                else
                {
                    db.UpdateData(table);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }  
    }
}

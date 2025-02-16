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

            bool run = true;
            while (run)
            {
                try
                {
                    Console.WriteLine("Do you want to insert, delete or update data?\nIf you want to exit the program write exit.");
                    Console.WriteLine();
                    string action = Console.ReadLine();
                    if (action == "insert")
                    {
                        Console.WriteLine("Write a number of table you want to insert into.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                        int table = int.Parse(Console.ReadLine());
                        if (table < 1 || table > 5)
                        {
                            Console.WriteLine("Invalid input.");
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            db.InsertData(table);
                        }
                    }

                    else if (action == "delete")
                    {
                        Console.WriteLine("Write a number of table you want to delete from.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                        int table = int.Parse(Console.ReadLine());
                        if (table < 1 || table > 5)
                        {
                            Console.WriteLine("Invalid input.");
                            Console.WriteLine();
                            break;
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
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            db.UpdateData(table);
                        }
                    }
                    else if (action == "exit")
                    {
                        run = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Write insert, delete or update");
                        Console.WriteLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
            }
        }  
    }
}

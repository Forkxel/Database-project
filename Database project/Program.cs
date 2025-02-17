namespace Database_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseInteraction db = new DatabaseInteraction();
            Import import = new Import();
            
            bool run = true;
            while (run)
            {
                try
                {
                    db.CreateTables();
                    Console.WriteLine("Do you want to insert, delete, print or update data?\nIf you want to import data from JSON type import.\nIf you want to clear database type clear.\nIf you want to exit the program type exit.");
                    Console.WriteLine();
                    string action = Console.ReadLine().ToLower();
                    if (action == "insert")
                    {
                        Console.WriteLine("Write a number of the table you want to insert into.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                        int table = int.Parse(Console.ReadLine());
                        if (table < 1 || table > 5)
                        {
                            Console.WriteLine("Invalid input.");
                            Console.WriteLine();
                            break;
                        }
                        db.InsertData(table);
                    }

                    else if (action == "delete")
                    {
                        Console.WriteLine("Write a number of the table you want to delete from.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                        int table = int.Parse(Console.ReadLine());
                        if (table < 1 || table > 5)
                        {
                            Console.WriteLine("Invalid input.");
                            Console.WriteLine();
                            break;
                        }
                        db.DeleteData(table);
                    }
                    else if (action == "update")
                    {
                        Console.WriteLine("Write a number of the table you want to update.\n1. Member\n2. Book\n3. Loan\n4. Author\n5. Category");
                        int table = int.Parse(Console.ReadLine());
                        if (table < 1 || table > 5)
                        {
                            Console.WriteLine("Invalid input.");
                            Console.WriteLine();
                            break;
                        }
                        db.UpdateData(table);
                    }
                    else if (action == "exit")
                    {
                        run = false;
                    }
                    else if (action == "import")
                    {
                        Console.WriteLine("To import data from Json open directory \\Your directory in which is this application\\Database project\\bin\\Debug\\net8.0 " +
                                          "\nIn this directory you will find file named import.json " +
                                          "\nOpen this file and add custom names for Author and Category tables. " +
                                          "\nSave this file and restart the program. \nChoose import again and write ready to the Console.");
                        Console.WriteLine();
                        
                        string answear = Console.ReadLine().ToLower();
                        if (answear.Equals("ready"))
                        {
                            import.ImportJson();
                            Console.WriteLine("Import complete.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                            Console.WriteLine();
                        }
                    }
                    else if (action == "clear")
                    {
                        Console.WriteLine("Are you sure you want to clear the database?\nType again clear.");
                        Console.WriteLine();
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "clear")
                        {
                            db.DropTables();
                            Console.WriteLine("Database cleared.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Clearing database terminated.");
                            Console.WriteLine();
                        }
                    }
                    else if (action == "print")
                    {
                        db.PrintAll();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
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

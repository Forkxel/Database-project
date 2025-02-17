using System.Data.SqlClient;
using Database_project.Tables;
using Newtonsoft.Json;

namespace Database_project;

/// <summary>
/// Class used to import Json files
/// </summary>
public class Import
{
    private SqlConnection connection = DatabaseConnection.GetInstance();

    /// <summary>
    /// Method used to import to tables author and category from Json file
    /// </summary>
    public void ImportJson()
    {
        try
        {
            var authorJson = JsonConvert.DeserializeObject<Dictionary<string, List<Author>>>(File.ReadAllText("import.json"));
            var authors = authorJson["author"];

            var categoryJson = JsonConvert.DeserializeObject<Dictionary<string, List<Category>>>(File.ReadAllText("import.json"));
            var categories = categoryJson["category"];

            foreach (var item in authors)
            {
                using (SqlCommand command = new SqlCommand($"INSERT INTO Author(firstName, lastName) VALUES (@firstName, @lastName);",
                           connection))
                {
                    command.Parameters.AddWithValue("@firstName", item.FirstName);
                    command.Parameters.AddWithValue("@lastName", item.LastName);
                    command.ExecuteNonQuery();
                }
            }

            foreach (var item in categories)
            {
                using (SqlCommand command = new SqlCommand($"INSERT INTO Category(name) VALUES (@name);", connection))
                {
                    command.Parameters.AddWithValue("@name", item.Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
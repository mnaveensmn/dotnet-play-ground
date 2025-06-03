using System.Text.Json;
using dynamo_db_load_testing.Entities;

namespace dynamo_db_load_testing.Utilities;

public class FeedsReader<T>
{
    public List<T> Read(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("JSON file not found.");
            return null;
        }

        var jsonContent = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<T>>(jsonContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
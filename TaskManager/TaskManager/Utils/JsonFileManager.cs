using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.Utils
{
    public class JsonFileManager
    {
        public static void WriteToJson(User user, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(user.Assignments, options);
            File.WriteAllText(filePath, json);
        }

        public static List<Assignment> ReadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Assignment>();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string jsonDb = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Assignment>>(jsonDb, options) ?? new List<Assignment>();
        }
    }
}

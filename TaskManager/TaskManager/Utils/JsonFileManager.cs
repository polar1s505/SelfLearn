using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.Utils
{
    public class JsonFileManager
    {
        public static async Task WriteToJsonAsync(User user, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(user.Assignments, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<Assignment>> ReadFromJsonAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Assignment>();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string jsonDb = await File.ReadAllTextAsync(filePath);

            return JsonSerializer.Deserialize<List<Assignment>>(jsonDb, options) ?? new List<Assignment>();
        }
    }
}

using System.Text.Json;
using serverDashboard.Models;

namespace serverDashboard.Services
{
    public class ServerService
    {
        private readonly Root _data;

        public ServerService()
        {
             var path = Path.Combine(AppContext.BaseDirectory, "Data", "serverData.json");
            Console.WriteLine($"Looking for JSON at: {path}");
            
            if (!File.Exists(path))
                throw new FileNotFoundException($"Could not find the JSON file at {path}");

            var json = File.ReadAllText(path);
            _data = JsonSerializer.Deserialize<Root>(json) ?? throw new Exception("Failed to deserialize JSON into Root object");
        }

        public List<Client> GetClients() => _data.Clients;
        public Client GetClient(string clientId) => _data.Clients.FirstOrDefault(c => c.ClientId == clientId);
    }
}

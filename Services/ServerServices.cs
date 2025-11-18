using serverDashboard.Models;
using System.Text.Json;

namespace serverDashboard.Services
{
    public class ServerService
    {
        private readonly Root _root;

        public ServerService()
        {
            // Load JSON data
            var json = System.IO.File.ReadAllText("wwwroot/data/clients.json");
            _root = JsonSerializer.Deserialize<Root>(json);
        }

        public List<Client> GetClients()
        {
            return _root.Clients;
        }
    }
}

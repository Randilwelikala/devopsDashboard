using System.Text.Json;
using serverDashboard.Models;

namespace serverDashboard.Services
{
    public class ServerService
    {
        private readonly List<Client> _clients;

        public ServerService()
        {            
            string json = File.ReadAllText("Data/serverData.json");
            _clients = JsonSerializer.Deserialize<List<Client>>(json);
        }

        public List<Client> GetClients() => _clients;
        public Client GetClient(string clientId) => _clients.FirstOrDefault(c => c.ClientId == clientId);
    }
}

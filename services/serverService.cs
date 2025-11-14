using System.Text.Json;
using ServerDashboard.models;

namespace ServerDashboard.services
{
    public class ServerService
    {
        private readonly Root _data;

        public ServerService()
        {
            string json = File.ReadAllText("serverdata.json");
            _data = JsonSerializer.Deserialize<Root>(json);
        }

        public List<Client> GetClients() => _data.Clients;

        public Client GetClient(string clientId) =>
            _data.Clients.FirstOrDefault(c => c.ClientId == clientId);
    }
}

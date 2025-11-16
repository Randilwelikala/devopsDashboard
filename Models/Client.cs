namespace serverDashboard.Models
{
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<Server> Servers { get; set; } = new List<Server>();
    }
}

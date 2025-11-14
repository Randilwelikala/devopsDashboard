namespace ServerDashboard.models
{
    public class Root
    {
        public List<Client> Clients { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<Server> Servers { get; set; }
    }

    public class Server
    {
        public string ServerId { get; set; }
        public string ServerName { get; set; }
        public string Location { get; set; }
        public string IpAddress { get; set; }
        public string Os { get; set; }
        public string Status { get; set; }
        public int CpuUsage { get; set; }
        public int RamUsage { get; set; }
        public int DiskUsage { get; set; }
        public double NetworkIn { get; set; }
        public double NetworkOut { get; set; }
        public int UptimeHours { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<Anomaly> Anomalies { get; set; }
        public List<HistoryRecord> History { get; set; }
    }

    public class Anomaly
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class HistoryRecord
    {
        public DateTime Timestamp { get; set; }
        public int CpuUsage { get; set; }
        public int RamUsage { get; set; }
        public string Status { get; set; }
    }
}

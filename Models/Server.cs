namespace serverDashboard.Models
{
    public class Server
    {
        public string ServerId { get; set; }
        public string ServerName { get; set; }
        public string Status { get; set; }
        public double CpuUsagePercent { get; set; }
        public double RamUsedGb { get; set; }
        public double TotalRamGb { get; set; }
        public double StorageUsedGb { get; set; }
        public double TotalStorageGb { get; set; }
        public double RequestLatencyMs { get; set; }
        public double ErrorRatePercent { get; set; }
        public int DocumentsSignedLastHour { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace serverDashboard.models
{
    public class ClientInstance
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public ServerStatus Status { get; set; }

        public double TotalRamGb { get; set; }
        public double RamUsedGb { get; set; }
        public double TotalStorageGb { get; set; }
        public double StorageUsedGb { get; set; }
        public double CpuUsagePercent { get; set; }
        public double LoadAverage1m { get; set; }
        public int RequestLatencyMs { get; set; }
        public double ErrorRatePercent { get; set; }
        public int DocumentsSignedLastHour { get; set; }

        public DateTime LastCheckTime { get; set; }
        public List<AnomalyEvent> RecentAnomalies { get; set; } = new List<AnomalyEvent>();
    }

    public enum ServerStatus
    {
        Online,
        Degraded,
        Offline
    }
}

using System;

namespace serverDashboard.models
{
    public class AnomalyEvent
    {
        public string Id { get; set; } = string.Empty;
        public AnomalyType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string ClientName { get; set; } = string.Empty;
    }

    public enum AnomalyType
    {
        Error,
        Warning,
        HighLatency,
        UnusualTraffic
    }
}

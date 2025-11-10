using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace SigNEXDashboard.Models
{
    public enum ServerStatus
    {
        Online,
        Degraded,
        Offline
    }

    public enum AnomalyType
    {
        Error,
        Warning,
        UnusualTraffic,
        HighLatency
    }

    public class AnomalyEvent
    {
        public string Id { get; set; }
        public AnomalyType Type { get; set; }
        public string Description { get; set; }
        public DateTime TImestamp { get; set; }
        public string clientName { get; set; }

        [JsonIgnore]
        public string BadgeClass
        {
            get => Type switch
            {
                
                
                AnomalyType.Error => "bg-red-500",
                AnomalyType.Warning => "bg-yellow-500",
                AnomalyType.HighLatency => "bg-orange-500",
                AnomalyType.UnusualTraffic => "bg-blue-500",
                _ => "bg-gray-400"

                
            };
        }
    }

}
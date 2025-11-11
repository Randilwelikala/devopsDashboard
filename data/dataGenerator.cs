using System;
using System.Collections.Generic;
using serverDashboard.Models;

namespace serverDashboard.Data
{
    public static class DataGenerator
    {
        private static readonly Random rnd = new Random();
        private static readonly string[] clientNames = { "SLIC", "DFCC", "CSA" };
        private static readonly string[] regions = { "SLIC head Office", "DFCC head office", "CSA head office" };

        public static DashboardData Generate(int numberOfClients)
        {
            var data = new DashboardData
            {
                GeneratedTime = DateTime.UtcNow,
                ClientInstances = new List<ClientInstance>()
            };

            for (int i = 0; i < numberOfClients; i++)
            {
                string clientId = $"C{i + 1:D3}";
                string clientName = clientNames[i];
                string region = regions[i];

                ServerStatus status = ServerStatus.Online;
                if (i == 0) status = ServerStatus.Degraded;
                if (i == 2) status = ServerStatus.Offline;

                var instance = CreateClientInstance(clientId, clientName, region, status);
                data.ClientInstances.Add(instance);
            }

            return data;
        }

        private static ClientInstance CreateClientInstance(string clientId, string clientName, string region, ServerStatus status)
        {
            double totalRam = rnd.Next(16, 64);
            double totalStorage = rnd.Next(500, 1000);

            var instance = new ClientInstance
            {
                ClientId = clientId,
                ClientName = clientName,
                Region = region,
                Status = status,
                TotalRamGb = totalRam,
                TotalStorageGb = totalStorage,
                LastCheckTime = DateTime.UtcNow
            };

            if (status == ServerStatus.Offline)
            {
                instance.CpuUsagePercent = 0;
                instance.LoadAverage1m = 0;
                instance.RamUsedGb = 0;
                instance.StorageUsedGb = totalStorage;
                instance.RequestLatencyMs = 0;
                instance.ErrorRatePercent = 100;
                instance.DocumentsSignedLastHour = 0;
            }
            else if (status == ServerStatus.Degraded)
            {
                instance.CpuUsagePercent = rnd.Next(85, 98);
                instance.LoadAverage1m = rnd.Next(8, 15);
                instance.RamUsedGb = totalRam * 0.8 + rnd.NextDouble() * (totalRam * 0.15);
                instance.StorageUsedGb = totalStorage * (0.8 + rnd.NextDouble() * 0.15);
                instance.RequestLatencyMs = rnd.Next(600, 1800);
                instance.ErrorRatePercent = rnd.NextDouble() * (20 - 5) + 5;
                instance.DocumentsSignedLastHour = rnd.Next(100, 500);
                instance.RecentAnomalies.Add(GenerateAnomaly(AnomalyType.HighLatency, clientName));
            }
            else
            {
                instance.CpuUsagePercent = rnd.Next(5, 35);
                instance.LoadAverage1m = rnd.Next(0, 4);
                instance.RamUsedGb = totalRam * (0.3 + rnd.NextDouble() * 0.3);
                instance.StorageUsedGb = totalStorage * (0.1 + rnd.NextDouble() * 0.3);
                instance.RequestLatencyMs = rnd.Next(80, 250);
                instance.ErrorRatePercent = rnd.NextDouble() * 0.3;
                instance.DocumentsSignedLastHour = rnd.Next(2500, 8000);
            }

            return instance;
        }

        private static AnomalyEvent GenerateAnomaly(AnomalyType type, string clientName)
        {
            string description = type switch
            {
                AnomalyType.Error => $"[{clientName}] Critical database failure!",
                AnomalyType.Warning => $"[{clientName}] Disk space below 20%",
                AnomalyType.UnusualTraffic => $"[{clientName}] Traffic spike detected!",
                AnomalyType.HighLatency => $"[{clientName}] API latency exceeded threshold",
                _ => "Unknown anomaly"
            };

            return new AnomalyEvent
            {
                Id = Guid.NewGuid().ToString().Substring(0, 8),
                Type = type,
                Description = description,
                Timestamp = DateTime.UtcNow,
                ClientName = clientName
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using SignNEXDashboard.Models;

namespace SigNEXDashboard.Data
{
    public static class DataGenerator
    {
        private static readonly Random rnd = new Random();
        private static readonly string[] clientNames = { "SLIC", "DFCC", "CSA" };
        private static readonly string[] regions = { "SLIC head Office", "DFCC head office", "CSA head office" };

        public static DashboardData Generate(int numberOfClients)
        {
            numberOfClients = 3;

            var data = new DashboardData
            {
                generatedTime = DateTime.UtcNow,
                clientInstances  = new List<clientInstance>()

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
                data.ClientInstance.Add(instance);
            }
            return data;

        }

        private static ClientInstance CreateClientInsatnce(string clientId, string clientName, string region, string status)
        {
            double totalRam = rnd.Next(16, 64);
            double totalStorage = rnd.Next(500, 1000);

            var instance = new ClientInstance
            {
                clientId = clientId,
                clientName = clientName,
                region = region,
                status = status,
                totalRamGb = totalRam,
                totalStorageGb = totalStorage,
                lastCheckTime = DateTime.UtcNow.AddMinutes(-rnd.Next(1, 5))

            };

            if (status == ServerStaus.offline)
            {
                instance.cpuUsagePercent = 0.0;
                instance.loadAverage1m = 0.0;
                instance.ramUsedGb = 0.0;
                instance.storageUsedGb = instance.TotalStorageGb;
                instance.requestLatencyMs = 0;
                instance.errorRatePercent = 100.0;
                instance.documentsSignedLastHour = 0;
            }

            else if (status == serverStatus.Degraded)
            {
                instance.cpuUsagePercent = rnd.Next(850, 980) / 10.0;
                instance.loadAverage1m = rnd.Next(80, 150) / 10.0;
                instance.ramUsageGb = rnd.NextDouble() * (totalRam * 0.95 - totalRam * 0.8) + (totalRam * 0.8);
                instance.storageUsedGb = totalStorage * (rnd.Next(800, 950) / 1000.0);
                instance.RequestLatencyMs = rnd.Next(600, 1800);
                instance.ErrorRatePercent = rnd.Next(5, 20) / 10.0;
                instance.DocumentsSignedLastHour = rnd.Next(100, 500);

                instance.RecentAnomalies.Add(GenerateAnomoly(AnomalyType.HighLatency, clientName, rnd));
                if (rnd.Next(10) < 8)
                {
                    instance.RecentAnomalies.Add(GEnerateAnomaly(AnomalyType.Error, clientaName, rnd));

                }

            }

            else
            {
                instance.CpuUsagePercent = rnd.Next(50, 350) / 10.0;
                instance.LoadAverage1m = rnd.Next(5, 40) / 10.0;
                instance.RamUsedGb = totalRam * (rnd.Next(300, 600) / 1000.0);
                instance.StorageUsedGb = totalStorage * (rnd.Next(100, 400) / 1000.0);
                instance.RequestLatencyMs = rnd.Next(80, 250);
                instance.ErrorRatePercent = rnd.Next(0, 3) / 10.0;
                instance.DocumentsSignedLastHour = rnd.Next(2500, 8000);

                if (rnd.Next(100) < 5)
                {
                    instance.RecentAnomalies.Add(GenerateAnomaly(AnomalyType.Warning, clientName, rnd));

                }


            }
            return instance;


        }
        private static AnomalyEvent GenerateAnomaly(AnomalyType type, string clientName,  Random rnd)
        {
            string description = type switch
            {
                AnomalyType.Error => $"[{clientName}] Critical database connection failure in signing service. Restart required.",
                AnomalyType.warning => $"[{clientName}]Disk space for transaction logs below 20% threshold.",
                AnomalyType.UnusualTraffic => $"[{clientName}] Request volume spiked 400% in the last 10 minutes. Possible DDoS or large integration batch.",
                AnomalyType.HighLatency => $"[{clientName}] API latency exceeded 1200ms for 5 reporting cycles. Check resource saturation.",
                _ => "Unknown anomaly detected."
            };

            return new AnomalyEvent
            {
                Id = Guid.NewGuid().ToString().Substring(0, 8),
                Type = Type,
                description = description,
                TimestampAttribute = DateTime.UtcNow.AddMinutes(-rnd.Next(1, 60)),
                clientNames = clientName
            };
        }

    }
    
}


using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using serverDashboard.models;

namespace serverDashboard.Pages
{
    public class IndexModel : PageModel
    {
        public List<ClientInstance> ClientInstances { get; set; } = new List<ClientInstance>();
        public DashboardData FullData { get; set; } = new DashboardData();
        public string DefaultClientId { get; set; } = string.Empty;

        public void OnGet()
        {
            FullData = DataGenerator.Generate(3);
            ClientInstances = FullData.ClientInstances;

            ClientInstances = ClientInstances
                .OrderByDescending(c => c.Status == ServerStatus.Offline)
                .ThenByDescending(c => c.Status == ServerStatus.Degraded)
                .ThenBy(c => c.ClientName)
                .ToList();

            DefaultClientId = ClientInstances.FirstOrDefault()?.ClientId ?? string.Empty;
        }

        public string GetAnomalyIcon(AnomalyType type)
        {
            return type switch
            {
                AnomalyType.Error => "⚠",
                AnomalyType.Warning => "❗",
                AnomalyType.HighLatency => "⏳",
                AnomalyType.UnusualTraffic => "📈",
                _ => "ℹ"
            };
        }
    }
}

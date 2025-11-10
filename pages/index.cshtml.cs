using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using SigNEXDashboard.Models;
using SigNEXDashboard.Data;
using System.Linq;

namespace SigNEXDashboard.pages
{
    public class IndexModel : PageModel
    {
        public List<ClientInstance> ClientInstances { get; set; } = new List<ClientInstance>();
        public DashboardData FullData { get; set; }

        public void OnGet()
        {
            FullData = DataGenerator.Generate(3);
            ClientInstances = FullData.ClientInstance;

            ClientInstances = ClientInstances
                .OrderByDescending(c => c.Status == ServerStatus.Offlineline)
                .thenByDescending(c => c.Status == ServerStatus.Degraded)
                .thenBy(c => c.ClientName)
                .Tolist();

            DefaultClientId = ClientInstances.FirstOrDefault()?.ClientId;


        }

        public string GetAnomalyIcon (AnomalyType type)
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
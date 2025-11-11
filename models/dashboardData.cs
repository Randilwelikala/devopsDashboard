using System;
using System.Collections.Generic;

namespace serverDashboard.Models
{
    public class DashboardData
    {
        public DateTime GeneratedTime { get; set; }
        public List<ClientInstance> ClientInstances { get; set; } = new List<ClientInstance>();
    }
}

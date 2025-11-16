using Microsoft.AspNetCore.Mvc;
using serverDashboard.Models;
using serverDashboard.Services;

namespace serverDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ServerService _service;

        public DashboardController(ServerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var clients = _service.GetClients();
            return View(clients);
        }
    }
}

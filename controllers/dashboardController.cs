using Microsoft.AspNetCore.Mvc;
using ServerDashboard.services;

namespace ServerDashboard.Controllers
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

        public IActionResult Client(string id)
        {
            var client = _service.GetClient(id);
            if (client == null) return NotFound();
            return View(client);
        }
    }
}

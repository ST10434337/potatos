using Microsoft.AspNetCore.Mvc;

namespace SmartX.Api.Controllers
{
    public class TelemetryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

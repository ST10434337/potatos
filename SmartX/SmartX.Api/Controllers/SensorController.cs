using Microsoft.AspNetCore.Mvc;

namespace SmartX.Api.Controllers
{
    public class SensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SmartX.Api.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

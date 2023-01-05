using Microsoft.AspNetCore.Mvc;

namespace AppDotNet.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

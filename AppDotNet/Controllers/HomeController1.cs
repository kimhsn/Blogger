using Microsoft.AspNetCore.Mvc;

namespace AppDotNet.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

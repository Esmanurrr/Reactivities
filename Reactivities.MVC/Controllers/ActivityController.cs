using Microsoft.AspNetCore.Mvc;

namespace Reactivities.MVC.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

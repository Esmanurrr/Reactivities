using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reactivities.MVC.Models;
using Reactivities.MVC.ViewModels;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Reactivities.MVC.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5000/api");
        private readonly HttpClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ActivityViewModel> activityList = new List<ActivityViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Activities/GetActivities").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                activityList = JsonConvert.DeserializeObject<List<ActivityViewModel>>(data);
            }


            return View(activityList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
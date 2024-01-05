using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reactivities.MVC.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace Reactivities.MVC.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5000/api");
        private readonly HttpClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client.CreateClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ActivityViewModel> activityList = new List<ActivityViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Activities/GetActivities").Result;
            //HttpResponseMessage response = _client.GetAsync(new Uri(baseAddress, "/Activities/GetActivities")).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                activityList = JsonConvert.DeserializeObject<List<ActivityViewModel>>(data);
            }

            return View(activityList);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            ActivityViewModel activity = new ActivityViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Activities/GetActivity" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Activities/CreateActivity", content);
              // var result = response.Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Activity Created.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ActivityViewModel activity = new ActivityViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Activities/GetActivity" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var activityDetails = response.Content.ReadAsStringAsync().Result;
                return View(activityDetails);
            }

            return View(activity);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActivityViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Activities/EditActivity", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Activity Details Edit.";
                return RedirectToAction("Index");
            }

            return View();

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
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Commented part is The method of linking ASP.Net With FireBase And For This We Use Nuget Package Name: FireSharp
            /*string authSecret = "xtkV91sBXjl7MrO1H6jZlF3al30m7e0tPbpMU5F2";
            string basePath = "https://firstwebappdbproject-default-rtdb.firebaseio.com/";
            string senderName = " firstwebappdbproject";

            IFirebaseClient client;
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };
            client = new FireSharp.FirebaseClient(config);
            if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
            {
                var data = new
                {
                    Name = "test",
                    Phone = "126318273"
                };
                var response = client.Push("doc/", data);
            }*/
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
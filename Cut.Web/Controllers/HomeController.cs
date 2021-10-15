using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cut.Web.Models;
using Cut.Data;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cut.Web.Controllers
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
            return View();
        }

        public IActionResult Settings()
        {
            Cut.Data.Settings st = new Cut.Data.Settings();
            if (HttpContext.Request.Cookies.ContainsKey("cut-settings")) {
                string jsonST = HttpContext.Request.Cookies["cut-settings"];
                st = JsonSerializer.Deserialize<Cut.Data.Settings>(jsonST);
            }
            return View(st);
        }

        [HttpPost]
        public IActionResult SaveSettings(int bladeWidth, int maxCutOff, int defaultPlankLength)
        {
            Cut.Data.Settings st = new Cut.Data.Settings(){BladeWidth = bladeWidth, MaxCutOff = maxCutOff, DefaultPlankLength = defaultPlankLength};
            HttpContext.Response.Cookies.Append("cut-settings", JsonSerializer.Serialize<Cut.Data.Settings>(st));

            return RedirectToAction("Settings");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

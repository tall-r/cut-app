using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cut.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

using System.Text.Json;
using System.Text.Json.Serialization;

using Cut.Data;

namespace Cut.Web.Controllers
{
    //[Route("[controller]")]
    public class PlatesController : Controller
    {
        private readonly ILogger<PlatesController> _logger;

        public PlatesController(ILogger<PlatesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CutList(FileModel model)
        {
            var req = HttpContext.Request;
            if (model == null || model.File == null) {
                throw new Exception("No file uploaded.");
            }

            Settings cfg = new Settings();
            if (HttpContext.Request.Cookies.ContainsKey("cut-settings")) {
                string jsonST = HttpContext.Request.Cookies["cut-settings"];
                cfg = JsonSerializer.Deserialize<Cut.Data.Settings>(jsonST);
            }

            var cuts = PlankUtils.LoadCuts(model.File.OpenReadStream());

            PlankLib lib = null;

            string path = Path.Combine(Startup.WebRootPath, "plank-lib.xml");
            if (System.IO.File.Exists(path)) {
                try{
                    lib = PlankLib.LoadLib(path, cfg);
                }
                catch {
                    lib = new PlankLib();
                }
            }

            //PlankLib lib = new PlankLib();

            var plates = PlankUtils.GenerateCutList(cfg, lib, cuts);

            ViewData["cfg"] = cfg;
            return View(plates);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
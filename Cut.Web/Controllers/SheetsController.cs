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
//using System.Web.Mvc.Html;

namespace Cut.Web.Controllers
{
    //[Route("[controller]")]
    public class SheetsController : Controller
    {
        private readonly ILogger<SheetsController> _logger;

        public SheetsController(ILogger<SheetsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("SheetWidth,SheetHeight,AllowRotation,File")] SheetFileModel sheetModel) {
            if (ModelState.IsValid) {
                // _context.Add(sheetModel);
                // await _context.SavechangesAsync();
                return View(sheetModel);
            }

            return View(sheetModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CutList([Bind("SheetWidth,SheetHeight,AllowRotation,File")] SheetFileModel model) {
            if (!ModelState.IsValid || model.SheetHeight < model.SheetWidth) {
                return Error();
            }
            
            Settings cfg = new Settings();
            if (HttpContext.Request.Cookies.ContainsKey("cut-settings")) {
                string jsonST = HttpContext.Request.Cookies["cut-settings"];
                cfg = JsonSerializer.Deserialize<Cut.Data.Settings>(jsonST);
            }

            cfg.MaxCutOff = 20;

            List<SheetCut> list = Cut.Data.SheetCut.LoadCuts(model.File.OpenReadStream(), System.Text.Encoding.UTF8);

            List<SheetCut> wrongCuts = new List<SheetCut>();

            var data = SheetUtils.GenerateSheets(list, wrongCuts, model.SheetSize, model.AllowRotation, cfg);

            var cutModel = new SheetCutsModel();
            cutModel.SheetSize = model.SheetSize;
            cutModel.Sheets = data;
            cutModel.WrongSheets = wrongCuts;

            return View(cutModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult Sheets() {
            List<SheetCut> list = new List<SheetCut>();
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K1", new Size(600,600)));
            list.Add(new SheetCut("K2", new Size(800,600)));
            list.Add(new SheetCut("K2", new Size(800,600)));
            list.Add(new SheetCut("K2", new Size(800,600)));
            list.Add(new SheetCut("K2", new Size(800,600)));
            list.Add(new SheetCut("K2", new Size(800,600)));
            list.Add(new SheetCut("K2", new Size(800,600)));
            list.Add(new SheetCut("K3", new Size(1200,600)));
            list.Add(new SheetCut("K3", new Size(1200,600)));
            list.Add(new SheetCut("K3", new Size(1200,600)));
            list.Add(new SheetCut("K3", new Size(1200,600)));
            list.Add(new SheetCut("K4", new Size(400,600)));
            list.Add(new SheetCut("K4", new Size(400,600)));
            list.Add(new SheetCut("K4", new Size(400,600)));
            list.Add(new SheetCut("K4", new Size(400,600)));
            list.Add(new SheetCut("K5", new Size(600,100)));
            list.Add(new SheetCut("K5", new Size(600,100)));
            list.Add(new SheetCut("K5", new Size(600,100)));
            list.Add(new SheetCut("K5", new Size(600,100)));
            list.Add(new SheetCut("K5", new Size(600,100)));
            list.Add(new SheetCut("K5", new Size(600,100)));
            list.Add(new SheetCut("K6", new Size(800,100)));
            list.Add(new SheetCut("K6", new Size(800,100)));
            list.Add(new SheetCut("K6", new Size(800,100)));
            list.Add(new SheetCut("K6", new Size(800,100)));
            list.Add(new SheetCut("K7", new Size(1200,100)));
            list.Add(new SheetCut("K7", new Size(1200,100)));
            list.Add(new SheetCut("K7", new Size(1200,100)));
            list.Add(new SheetCut("K7", new Size(1200,100)));
            list.Add(new SheetCut("K8", new Size(400,100)));
            list.Add(new SheetCut("K8", new Size(400,100)));
            list.Add(new SheetCut("K8", new Size(400,100)));
            list.Add(new SheetCut("K8", new Size(400,100)));
            List<SheetCut> wrongCuts = new List<SheetCut>();
            Size size = new Size(1220, 2440);
            bool rotaAllowed = true;

            Settings cfg = new Settings();
            if (HttpContext.Request.Cookies.ContainsKey("cut-settings")) {
                string jsonST = HttpContext.Request.Cookies["cut-settings"];
                cfg = JsonSerializer.Deserialize<Cut.Data.Settings>(jsonST);
            }

            cfg.MaxCutOff = 20;

            var data = SheetUtils.GenerateSheets(list, wrongCuts, size, rotaAllowed, cfg);
            Response.ContentType = "application/json";
            Dictionary<string,object> cfgInfo = new Dictionary<string, object>();
            cfgInfo["bladeWidth"] = cfg.BladeWidth;
            cfgInfo["maxCutOff"] = cfg.MaxCutOff;
            List<Dictionary<string,object>> json = new List<Dictionary<string, object>>();
            json.Add(cfgInfo);
            foreach(var d in data) {
                json.Add(d.AsJsonData());
            }
            return Json(json);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cut.Web.Models;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
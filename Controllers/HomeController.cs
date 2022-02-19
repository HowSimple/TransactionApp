using Commerce_TransactionApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace Commerce_TransactionApp.Controllers
{
   
    public class HomeController : Controller
    { 
        String connectionString;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
            ViewBag.connection = ConfigurationManager.ConnectionStrings["database"];
            ViewBag.test = ConfigurationManager.ConnectionStrings["testkey"];


        }
     

        public IActionResult Index()
        {
            ViewBag.connection = connectionString;
            ViewBag.test = connectionString;
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

using Commerce_TransactionApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Commerce_TransactionApp.Controllers
{
   
    public class HomeController : Controller
    { 
        //private readonly 
        
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly TransactionDbService db;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
            this.db = new TransactionDbService(this._configuration);
            

            

        }

        public IActionResult Login()
        {
            ViewBag.user = "";
            ViewBag.id = "";

            return View();
        }
        [HttpPost]
        public IActionResult Login(User response)
        {
            int userID = db.Login(response);
            Session("UserId") = userID;

            //TESTING BUTTON< I USE IT LOL>> > I USE WHAT I GOT.
            //db.UnselectNotification(userID, 1);
            //db.UnselectNotification(userID, 2);
            //db.UnselectNotification(userID, 3);

            ViewBag.user = response.username;
            ViewBag.id = userID;
            //return View("Summary", "Transactions");
            return RedirectToAction("Summary","Transactions");
        }


        public IActionResult Index()
        {

            

            return View();
        }


        public IActionResult Summary()
        { // requires the user to login to access Summary
            return RedirectToAction("Login");
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

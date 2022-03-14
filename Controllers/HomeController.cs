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
using Microsoft.AspNetCore.Http;

namespace Commerce_TransactionApp.Controllers
{
   
    public class HomeController : Controller
    { 
        //private readonly 
        
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly TransactionDbService db;

        private const string SessionKey_UserId = "id";
        private const string SessionKey_Username = "username";

        private int getUserId()
        {
            return (int)HttpContext.Session.GetInt32(SessionKey_UserId.ToString());

        }
        private string getUsername()
        {
            return (string)HttpContext.Session.GetString(SessionKey_Username.ToString());

        }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
            this.db = new TransactionDbService(this._configuration);
            

            

        }
        // 
        private void loginUser(string user, string pass)
        {
            int userID = db.Login(user,pass);
            if (userID != 0)
            {
                HttpContext.Session.SetInt32("id", userID);
                HttpContext.Session.SetString("username", user);


            }
            else
            {
                
            }


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
            int userID = db.Login(response.username,response.password);

            loginUser(response.username, response.password);


            //TESTING BUTTON< I USE IT LOL>> > I USE WHAT I GOT.
            //db.UnselectNotification(userID, 1);
            //db.UnselectNotification(userID, 2);
            //db.UnselectNotification(userID, 3);

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

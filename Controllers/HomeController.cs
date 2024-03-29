﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TransactionApp.Models;
using TransactionApp.Services;
namespace TransactionApp.Controllers
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
        public bool isLoggedIn()
        {
            return HttpContext.Session.GetInt32(SessionKey_UserId.ToString()) != null;
        }
        bool validatePassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
);
            return regex.IsMatch(password);
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
            int userID = db.Login(user, pass);
            if (userID != 0)
            {
                HttpContext.Session.SetInt32("id", userID);
                HttpContext.Session.SetString("username", user);
            }

        }

        public IActionResult Login()
        {
            if (isLoggedIn())
                return RedirectToAction("Summary", "Transactions");
            else
            {
                ViewBag.Login = "Login";
                return View("Login");
            }

        }
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult Login(User response)
        {
            ViewBag.Login = "Login";
            int userID = db.Login(response.username, response.password);

            if (userID != 0)
            {
                loginUser(response.username, response.password);
                return RedirectToAction("Summary", "Transactions");

            }
            else return View("Login");

        }

        public IActionResult Register()
        {
            ViewBag.Login = "Login";
            if (isLoggedIn())
                return RedirectToAction("Summary", "Home");
            else return View("Register");
        }

        [HttpPost]
        public IActionResult Register(User response, string confirmPassword, string state, int accountNumber)
        {
            ViewBag.Login = "Login";


            if (response.password == confirmPassword && validatePassword(response.password))
            {
                int userID = db.Register(response.username, response.password, state, accountNumber);
                if (userID != 0)

                    db.Login(response.username, response.password);
                loginUser(response.username, response.password);
                return RedirectToAction("Summary", "Transactions");


            }
            if (response.password != confirmPassword)
            { ViewBag.error += "Try again: Passwords are not matching.    \n"; }
            if (!validatePassword(response.password))
                ViewBag.error += "Please enter a secure password:\n 8+ characters\n At least 1 capital\n At least 1 lowercase\n At least 1 number";
            return View("Register");

        }

        public IActionResult Index()
        {
            // requires the user to login to access Summary if they aren't logged in

            ViewBag.Login = "Login";
            return View("Index");
        }


        public IActionResult Summary()
        { // requires the user to login to access Summary if they aren't logged in
            if (isLoggedIn())
                return RedirectToAction("Summary", "Transactions");
            else return RedirectToAction("Login");
        }
        public IActionResult Notifications()
        { // requires the user to login to access Notifications if they aren't logged in

            if (isLoggedIn())
                return RedirectToAction("Notifications", "Transactions");
            else return RedirectToAction("Login");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

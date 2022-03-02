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


        public IActionResult Index()
        {

            



            return View();
        }
 
        

        public IActionResult Privacy()
        {
            //ViewBag.Transactions = transactionList;
            return View();
        }

        public IActionResult Summary()
        {
            
           // Transaction tx = new Transaction(101, 6.50, "CR", "Kansas City", "Pizza", new DateTime(2022, 3, 1), 250.99);
           // db.AddNewTransaction(tx);
            
            System.Data.DataTable transactionList = db.GetAllTransactions();
            ViewBag.Transactions = transactionList;
            ViewBag.Total = transactionList.Rows.Count;
            return View();
        }
        [HttpPost]
        public IActionResult Summary(Transaction response)
        {


            //string zipcode = Request.Form["accountId"];
            db.AddNewTransaction(response);
           // ViewBag.AddedTransaction = response;
            System.Data.DataTable transactionList = db.GetAllTransactions();
            ViewBag.Transactions = transactionList;
            ViewBag.Total = transactionList.Rows.Count;

            return View();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

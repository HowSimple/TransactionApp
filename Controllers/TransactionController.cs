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
namespace Commerce_TransactionApp
{
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly TransactionDbService db;

        public TransactionsController(ILogger<TransactionsController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
            this.db = new TransactionDbService(this._configuration);




        }

        public IActionResult Index()
        {
            return View();
        }
/*        public IActionResult Notifications()
        {
            System.Data.DataTable transactionList = db.GetAllTransactions();
            ViewBag.Total = transactionList.Rows.Count;

            // passes the transaction table to webpage to display
            return View(transactionList);
        }
        [HttpPost]
        public IActionResult Notifications()
        {
            db.AddNewTransaction(response);
            System.Data.DataTable transactionList = db.GetAllTransactions();
            ViewBag.Transactions = transactionList;
            ViewBag.Total = transactionList.Rows.Count;

            return View();

        }*/

        public IActionResult Summary()
        {
            System.Data.DataTable transactionList = db.GetTransactionSummary(123);
            ViewBag.Total = transactionList.Rows.Count;

            // passes the transaction table to webpage to display
            return View(transactionList);
        }
        [HttpPost]
        public IActionResult Summary(Transaction response)
        {
            db.AddNewTransaction(response);
            //System.Data.DataTable transactionList = db.GetTransactionSummary(123);
            //ViewBag.Transactions = transactionList;
            //ViewBag.Total = transactionList.Rows.Count;

            return View("Summary");

        }
    }
}

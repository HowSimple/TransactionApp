using TransactionApp.Models;
using TransactionApp.Services;
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

namespace TransactionApp
{
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly TransactionDbService db;
        public SelectedNotifications notificationRules;


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
        
        public TransactionsController(ILogger<TransactionsController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
           
            this.db = new TransactionDbService(this._configuration);

            // remove once notification rules are read from DB
            notificationRules = new SelectedNotifications(false, false, false,0,0);
            // remove once login is working

        }

        public IActionResult Index()
        {
            ViewBag.Login = "Logout";
            return View();
        }
        public IActionResult Dashboard() {
            ViewBag.TotalNotifications = db.GetAllNotifications(getUserId()).Rows.Count;
            ViewBag.Balance = db.GetBalance(getUserId());
            ViewBag.Login = "Logout";
            System.Data.DataTable notificationOverview = db.GetTriggerCounts(getUserId());
            return View();
        }
 
        public IActionResult Notifications()
        {
            
            ViewBag.Notifications = db.GetAllNotifications(getUserId());
            ViewBag.Login = "Logout";

            // set notification check boxes based on what the user has previously set 
            System.Data.DataTable userSelectedNotifications = db.GetUserNotifications(getUserId());
            foreach (System.Data.DataRow dataRow in userSelectedNotifications.Rows)
            {

               
                if (dataRow["amount"] != null) 
                    if (dataRow["userNotificationID"].ToString() == "1")
                    notificationRules.largeWithdrawLimit = Double.Parse(dataRow["amount"].ToString());
                    else if (dataRow["userNotificationID"].ToString() == "3")
                        notificationRules.lowBalanceLimit = Double.Parse(dataRow["amount"].ToString());
                                   
               
                    if (dataRow["userNotificationID"].ToString() == "1")
                        notificationRules.largeWithdraw = true;
                    else if (dataRow["userNotificationID"].ToString() == "2")
                        notificationRules.outOfState = true;
                    else if (dataRow["userNotificationID"].ToString() == "3")
                        notificationRules.lowBalance = true; 
                
              
            }

            // passes the transaction table to webpage to display
            return View(notificationRules);
        }
        [HttpPost]
        public IActionResult Notifications(SelectedNotifications response)
        {
            System.Data.DataTable notifications = db.GetAllNotifications(getUserId());
            ViewBag.Notifications = notifications;
            ViewBag.Login = "Logout";
            //if (response.lowBalance != notificationRules.lowBalance) {

            if (response.lowBalance)
                db.SelectNotification(getUserId(), 3,response.lowBalanceLimit);
            else db.UnselectNotification(getUserId(), 3);
            if (response.outOfState)
                db.SelectNotification(getUserId(), 2, 0);
            else db.UnselectNotification(getUserId(), 2);
            if (response.largeWithdraw)
                db.SelectNotification(getUserId(), 1, response.largeWithdrawLimit);
            else db.UnselectNotification(getUserId(), 1);

            

            // shows Notifications() after updating user notifications on DB

            return View("Notifications");
        }

        public IActionResult Summary()
        {
            ViewBag.Login = "Logout";
            ViewBag.user = getUsername();
            
            db.PrintSummary(getUserId());
            ViewBag.exportTranactionsFile = "Transaction Summary.xml";
            System.Data.DataTable transactionList = db.GetTransactionSummary(getUserId());
            ViewBag.Total = transactionList.Rows.Count;
                    
            System.Data.DataTable notifications = db.GetAllNotifications(getUserId());

            ViewBag.Dashboard  = db.GetTriggerCounts(getUserId());
            ViewBag.TotalNotifications = db.GetAllNotifications(getUserId()).Rows.Count;
            ViewBag.Balance = db.GetBalance(getUserId());
           
            // passes the transaction table to webpage to display
            return View(transactionList);
        }
        [HttpPost]
        public IActionResult Summary(Transaction response)
        {
            ViewBag.Login = "Logout";
            ViewBag.user = getUsername();

            db.AddNewTransaction(response, getUserId());
            
            db.PrintSummary(getUserId());
            ViewBag.exportTranactionsFile = "Transaction Summary.xml";
            System.Data.DataTable transactionList = db.GetTransactionSummary(getUserId());
            ViewBag.Total = transactionList.Rows.Count;

            System.Data.DataTable notifications = db.GetAllNotifications(getUserId());

            ViewBag.Dashboard = db.GetTriggerCounts(getUserId());
            ViewBag.TotalNotifications = db.GetAllNotifications(getUserId()).Rows.Count;
            ViewBag.Balance = db.GetBalance(getUserId());
          
            // passes the transaction table to webpage to display
            return View(transactionList);

        }
    }
}

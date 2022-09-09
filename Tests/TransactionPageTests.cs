using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System;
using SeleniumExtras.WaitHelpers;
using System.Configuration;
//using TransactionApp.Models;

namespace TransactionApp.Tests
{
    public class TransactionPageTests
    {
        IWebDriver driver;
        TransactionPage transactionPage;
        NotificationPage notificationPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
                  
            transactionPage = new TransactionPage(driver);
            notificationPage = new NotificationPage(driver);
            LoginPage loginPage = new LoginPage(driver);
           
            string username = ConfigurationManager.AppSettings["TestAccountUsername"]!;
            string password = ConfigurationManager.AppSettings["TestAccountPassword"]!;
            loginPage.Login(username, password);
        }
        [TearDown]
        public void Teardown() { driver.Dispose(); }

        
        [Test]
        public void GetTransaction()
        {
           

            Assert.Pass();
        }
        [Test]
        public void GetBalance_01()
        {
            IWebElement balanceDisplay = driver.FindElement(transactionPage.BalanceLocator);
            double balance = transactionPage.GetBalance();
            Assert.That(balanceDisplay.Text.Contains("Balance: "));
            Assert.That(balanceDisplay.Text.Contains(balance.ToString()));
        }
        [Test]
        public void AddTransaction()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            IWebElement addButton = driver.FindElement(By.Name("addTransaction"));
            addButton.Click();
            double amount = 50.99;
            transactionPage.SetTransactionAmount(amount);

            String descriptionText = "Test purchase";
            transactionPage.SetTransactionDescription(descriptionText);
            transactionPage.SetTransactionDate(DateTime.Now);

            IWebElement balance = driver.FindElement(transactionPage.BalanceLocator);
            Assert.That(balance.Text.Contains("Balance: $"));

            double currentBalance = transactionPage.GetBalance();
            double expectedBalance = currentBalance - amount;
           
            driver.FindElement(transactionPage.TransactionAmountLocater).Submit();
            balance = driver.FindElement(By.Name("balance"));
            //Assert.That(balance.Text.Contains(expectedBalance.ToString()));
            Assert.That(transactionPage.GetBalance(), Is.EqualTo(expectedBalance));
           



        }
        [Test]
        public void AddTransaction_LargeWithdraw()
        {
           
            var initialTotal = driver.FindElement(transactionPage.TotalLargeWithdrawsLocator).Text;
            double transactionAmount = 50.99 + notificationPage.getLargeWithdrawSetting();
            transactionPage.openTransactionPage();
           
            var updatedTotal = driver.FindElement(transactionPage.TotalLargeWithdrawsLocator).Text;
            Assert.That(Double.Parse(updatedTotal), Is.GreaterThan(Double.Parse(initialTotal)));

        }
        [Test]
        public void AddTransaction_LowBalance()
        {
            double currentBalance = transactionPage.GetBalance();
            var initialTotal = driver.FindElement(transactionPage.TotalLowBalanceLocator).Text;
            double transactionAmount = currentBalance + notificationPage.getLowBalanceSetting();
            transactionPage.openTransactionPage();

            var updatedTotal = driver.FindElement(transactionPage.TotalLowBalanceLocator).Text;
            Assert.That(Double.Parse(updatedTotal), Is.GreaterThan(Double.Parse(initialTotal)));

        }
    }
}
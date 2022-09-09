using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TransactionApp.Tests
{
    public class TransactionPage
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }
        
        public By TransactionAmountLocater => By.Name("transactionAmount");
        public By TransactionDescriptionLocator => By.Name("transactionDescription");
        public By TransactionDateLocator => By.Name("processingDate");
        public By BalanceLocator => By.Name("balance");
        public By TotalLargeWithdrawsLocator => By.Name("largeWithdraw-monthly");
        public By TotalLowBalanceLocator => By.Name("lowBalance-monthly");

        public double GetBalance()
        {

            IWebElement balance = Driver.FindElement(BalanceLocator);
            return Double.Parse(balance.Text.Substring(10));
        }

        public TransactionPage(IWebDriver driver)
        {
            Driver = driver;
            //Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

        }
        public void openTransactionPage()
        {
            Driver.Url = "http://nowhereagain.vps.webdock.cloud:4000/Transactions/Summary";
        }
        /* public void AddTransaction(Transaction transaction)
         {
             SetTransactionAmount(transaction.transactionAmount);
             SetTransactionDate(transaction.processingDate);
             SetTransactionDescription(transaction.transactionDescription);
             Driver.FindElement(TransactionAmountLocater).Submit();
         }
        */
        public void SetTransactionAmount(double amount)
        {
            IWebElement amountBox =  Driver.FindElement(TransactionAmountLocater);
            amountBox.SendKeys(amount.ToString()); 
        }
        public void SetTransactionDescription(string description)
        {
            IWebElement descriptionBox = Driver.FindElement(TransactionDescriptionLocator);
            descriptionBox.SendKeys(description);
        }
        public void SetTransactionDate(DateTime date)
        {
            IWebElement dateBox = Driver.FindElement(TransactionDateLocator);
            var dateString = date.ToString("MMddyyyy hhmmtt").Split();
            dateBox.SendKeys(dateString[0]);
            dateBox.SendKeys(Keys.Tab);
            dateBox.SendKeys(dateString[1]);
        }
    }
}

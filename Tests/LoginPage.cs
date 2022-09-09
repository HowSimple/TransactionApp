using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TransactionApp
{
    public class LoginPage
    {
        protected IWebDriver Driver { get; private set; }
        public By LoginUsernameLocator => By.Name("username");
        public By LoginPasswordLocator => By.Name("password");

        public LoginPage(IWebDriver driver)
        {
            Driver = driver;
       

        }
        public void Login(string username, string password) {
            Driver.Url = "http://nowhereagain.vps.webdock.cloud:4000/";
            
            IWebElement link = Driver.FindElement(By.LinkText("Login"));
            link.Click();
            SetLogin(username, password);
            Driver.FindElement(LoginPasswordLocator).Submit();
        }
        
        internal void SetLogin(string username, string password) {
            IWebElement usernameField = Driver.FindElement(LoginUsernameLocator);
            usernameField.SendKeys(username);
            IWebElement passwordField = Driver.FindElement(LoginPasswordLocator);
            passwordField.SendKeys(password);
        }

    }
}

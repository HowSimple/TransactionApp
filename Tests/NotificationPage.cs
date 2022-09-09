using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TransactionApp.Tests
{
    public class NotificationPage
    {
        protected IWebDriver Driver { get; private set; }
        public By LowBalanceSettingLocater => By.Id("lowBalanceLimit");
        public By LargeWithdrawSettingLocater => By.Id("largeWithdrawLimit");
        public By ExpandSettingsLocator => By.PartialLinkText("settings");
        public NotificationPage(IWebDriver driver)
        {
            Driver = driver;
        }
        public void openNotificationPage()
        {
            Driver.Url = "http://nowhereagain.vps.webdock.cloud:4000/Transactions/Notifications";
        }
        public double getLowBalanceSetting()
        {
            openNotificationPage();
            Driver.FindElement(ExpandSettingsLocator).Click();
            IWebElement lowBalanceSetting = Driver.FindElement(LowBalanceSettingLocater);
            return Double.Parse(lowBalanceSetting.Text);
        }
        public double getLargeWithdrawSetting()
        {
            openNotificationPage();
            Driver.FindElement(ExpandSettingsLocator).Click();
            IWebElement largeWithdrawSetting = Driver.FindElement(LargeWithdrawSettingLocater);
            return Double.Parse(largeWithdrawSetting.GetAttribute("value"));
        }
        
    }
}

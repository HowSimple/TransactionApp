using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionApp;
using Microsoft.AspNetCore.Mvc;
using TransactionApp.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace Commerce_TransactionApp_Test
{
    // coverlet test ran, export to .xml with formatting in coverlet.settings
    // dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.settings

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            //var logger = new Mock<ILogger<HomeController>>();

            //var configuration = new Mock<IConfiguration>();
            //Arrange
            HomeController controller = new HomeController(null, null);




            //Act
            ViewResult result = controller.Privacy() as ViewResult;

            //Assert
            Assert.NotNull(result);
        }




        [Fact]
        public void Test2()
        {

            //Arrange
            HomeController controller = new HomeController(null, null);

            //Act

            ViewResult result = controller.Index() as ViewResult;


            //Assert
            Assert.NotNull(result);
        }

        /*[Fact]
        public void Test3()
        {

            //Arrange
            HomeController controller = new HomeController(null, null);

            //Act

            ViewResult result = controller.Register() as ViewResult;


            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void Test4()
        {

            //Arrange
            HomeController controller = new HomeController(null, null);

            //Act

            ViewResult result = controller.Summary() as ViewResult;


            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void Test5()
        {

            //Arrange
            HomeController controller = new HomeController(null, null);

            //Act

            ViewResult result = controller.Notifications() as ViewResult;


            //Assert
            Assert.NotNull(result);
        }


        //[Fact]
        //public void Test6()
        //{

        //    //Arrange
        //    HomeController controller = new HomeController(null, null);

        //    //Act

        //    ViewResult result = controller.Login() as ViewResult;


        //    //Assert
        //    Assert.NotNull(result);
        //}*/
    }
        public class transactionTest1
        {
        [Fact]
        public void Test10()
        {


            TransactionsController controller = new TransactionsController(null, null);

            ViewResult result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }
    }
}


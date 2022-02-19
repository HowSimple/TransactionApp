using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;



namespace Commerce_TransactionApp
{
    public class Program
    {   
        public static void Main(string[] args)
        {
           

            CreateHostBuilder(args).Build().Run();


        }
        public String GetConnectionString()
        {
            var key = "testkey";

            string value = ConfigurationManager.ConnectionStrings[key]
                .ConnectionString;
            Console.WriteLine(value);
            return key;
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

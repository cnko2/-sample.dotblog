using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetFx48
{
    [TestClass]
    public class SurveyUserSecretTests
    {
        [TestMethod]
        public void HostŪ�����K()
        {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureHostConfiguration(config =>
                                                          {
                                                              config.AddJsonFile("appsettings.json", false, true);
                                                          })
                ;
            var host = builder.Build();

            var config = host.Services.GetService<IConfiguration>();
            Console.WriteLine($"Player:Key = {config["Player:Key"]}");
            Console.WriteLine($"DbPassword = {config["DbPassword"]}");
        }

        [TestMethod]
        public void ��ʹ�ҤƲպAŪ�����K()
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .AddUserSecrets<SurveyUserSecretTests>()
                ;

            var config = builder.Build();
            Console.WriteLine($"Player:Key = {config["Player:Key"]}");
        }
    }
}
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetFx48
{
    [TestClass]
    public class SurveyOptionTests
    {
        [TestMethod]
        public void �`�JOption()
        {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureAppConfiguration((hosting, configBuilder) =>
                                                         {
                                                             // 1.Ū�պA�� 
                                                             var environmentName =
                                                                 hosting.Configuration["ENVIRONMENT2"];
                                                             configBuilder.AddJsonFile("appsettings.json", false, true);
                                                             configBuilder
                                                                 .AddJsonFile($"appsettings.{environmentName}.json",
                                                                              true, true);
                                                         })
                              .ConfigureServices((hosting, services) =>
                                                 {
                                                     // 2.�`�J Options
                                                     services.AddOptions();

                                                     // 3. �`�J IConfiguration
                                                     services.Configure<AppSetting1>(hosting.Configuration);

                                                     //�`�J��L�A��
                                                     services.AddSingleton<AppServiceWithOption>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppServiceWithOption>();
            var playerId = service.GetPlayerId();
            Console.WriteLine($"PlayerId = {playerId}");
        }

        [TestMethod]
        public void �`�JOptionMonitor()
        {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureAppConfiguration((hosting, configBuilder) =>
                                                         {
                                                             // 1.Ū�պA�� 
                                                             var environmentName =
                                                                 hosting.Configuration["ENVIRONMENT2"];
                                                             configBuilder.AddJsonFile("appsettings.json", false, true);
                                                             configBuilder
                                                                 .AddJsonFile($"appsettings.{environmentName}.json",
                                                                              true, true);
                                                         })
                              .ConfigureServices((hosting, services) =>
                                                 {
                                                     // �`�J Option �M���� Configuration
                                                     services.Configure<AppSetting1>(hosting.Configuration);

                                                     // �`�J Option �M�S�w Configuration Section Name
                                                     services.Configure<Player1>("Player",
                                                         hosting.Configuration.GetSection("Player"));

                                                     //�`�J��L�A��
                                                     services.AddScoped<AppServiceWithOptionsMonitor>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppServiceWithOptionsMonitor>();
            var playerId = service.GetPlayerId();
            Console.WriteLine($"PlayerId = {playerId}");
        }

        [TestMethod]
        public void �`�JOptionSnapshot()
        {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureAppConfiguration((hosting, configBuilder) =>
                                                         {
                                                             var environmentName =
                                                                 hosting.Configuration["ENVIRONMENT2"];
                                                             configBuilder.AddJsonFile("appsettings.json", false, true);
                                                             configBuilder
                                                                 .AddJsonFile($"appsettings.{environmentName}.json",
                                                                              true, true);
                                                         })
                              .ConfigureServices((hosting, services) =>
                                                 {
                                                     //  �`�J Option by ����պA
                                                     services.Configure<AppSetting1>(hosting.Configuration);

                                                     // �`�J Option by �S�w�պA
                                                     services.Configure<Player1>(hosting.Configuration
                                                         .GetSection("Player"));

                                                     //�`�J��L�A��
                                                     services.AddScoped<AppServiceWithOptionsSnapshot>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppServiceWithOptionsSnapshot>();
            var playerId = service.GetPlayerId();
            Console.WriteLine($"PlayerId = {playerId}");
        }
    }
}
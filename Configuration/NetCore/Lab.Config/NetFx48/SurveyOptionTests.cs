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
                                                     // 2. �`�J Option �M Configuration
                                                     services.Configure<AppSetting1>(hosting.Configuration);

                                                     //�`�J��L�A��
                                                     services.AddSingleton<AppWorkFlowWithOption>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppWorkFlowWithOption>();
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
                                                     services.AddScoped<AppWorkFlowWithOptionsMonitor>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppWorkFlowWithOptionsMonitor>();
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
                                                     services.AddScoped<AppWorkFlowWithOptionsSnapshot>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppWorkFlowWithOptionsSnapshot>();
            var playerId = service.GetPlayerId();
            Console.WriteLine($"PlayerId = {playerId}");
        }

[TestMethod]
        public void ����()
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
                                                     // 2. �`�J Option �M Configuration
                                                     services.Configure<AppSetting1>(hosting.Configuration);
                                                     //����
                                                     services.AddOptions<AppSetting1>()
                                                             .ValidateDataAnnotations()
                                                             .Validate(p =>
                                                                       {
                                                                           var hasContent = string.IsNullOrWhiteSpace(p.ConnectionStrings.DefaultConnectionString);
                                                                           if (hasContent == false)
                                                                           {
                                                                               return false;
                                                                           }

                                                                           return true;
                                                                       },
                                                                       "DefaultConnectionString must be value"); // Failure message.
                                                     ;

                                                     //�`�J��L�A��
                                                     services.AddSingleton<AppWorkFlowWithOption>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppWorkFlowWithOption>();
            var playerId = service.GetPlayerId();
            Console.WriteLine($"PlayerId = {playerId}");
        }
       
        [TestMethod]
        public void �����`�J�պA����()
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
                                                     var appSetting = hosting.Configuration.Get<AppSetting>();
                                                     services.AddSingleton(typeof(AppSetting), appSetting);
                                                     
                                                     //�`�J��L�A��
                                                     services.AddSingleton<AppWorkFlow1>();
                                                 })
                ;
            var host     = builder.Build();
            var service  = host.Services.GetService<AppWorkFlow1>();
            var playerId = service.GetPlayerId();
            Console.WriteLine($"PlayerId = {playerId}");
        }
        
    }
}
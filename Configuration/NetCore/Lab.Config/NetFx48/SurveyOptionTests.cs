using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
                                                             var environmentName = hosting.Configuration["ENVIRONMENT2"];
                                                             configBuilder.AddJsonFile("appsettings.json",false,true);
                                                             configBuilder.AddJsonFile($"appsettings.{environmentName}.json",true,true);
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
            Console.WriteLine($"PlayerId={playerId}");
        }
    }
}
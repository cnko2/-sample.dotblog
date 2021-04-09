using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AspNetCore5
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                                                 {
                                                     webBuilder.ConfigureAppConfiguration(p =>
                                                     {
                                                         // �����s���J�պA
                                                         //p.AddJsonFile("appsettings.json", false, false);
                                                     });
                                                     webBuilder.UseStartup<Startup>();
                                                 });
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
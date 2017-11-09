using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.IO;

namespace DevTeamUtils.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            //var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            /*var host = new WebHostBuilder()
                .UseKestrel()
                //.UseContentRoot(pathToContentRoot)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                //.UseUrls("http://*:51640;http://localhost:51640;http://amlnotpr398ht3:51640")
                .Build();

            //host.RunAsService();
            host.Run();
            */

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .PreferHostingUrls(false)
                //.UseUrls("http://*:51640;http://localhost:51640;http://amlnotpr398ht3:51640")
                .UseUrls("http://*:51640")
                //.UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}

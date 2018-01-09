using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
                //.UseUrls("http://*:51640")
                .Build();

            //host.RunAsService();
            host.Run();
            */

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
#if RELEASE
                .UseKestrel()
                .UseContentRoot(System.IO.Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .PreferHostingUrls(false)
                .UseUrls("http://*:51640")
#endif          
                .UseStartup<Startup>()
                .Build();
    }
}

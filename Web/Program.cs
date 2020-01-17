using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Patronage_NET.Web
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Run();

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build();


    }
}

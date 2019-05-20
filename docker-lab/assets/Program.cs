using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace dotnet_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Program>();
        
        public void Configure(IApplicationBuilder app, IConfiguration  configuration)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello {configuration.GetValue<string>("ENV_VALUE") ?? "GP"} World!");
            });
        }
    }
}

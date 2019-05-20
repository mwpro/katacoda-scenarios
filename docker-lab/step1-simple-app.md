# Create minimal ASP.NET API

<pre class="file" data-filename="dotnet-api.csproj" data-target="replace">
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
    <RootNamespace>dotnet_api</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.2.0" PrivateAssets="All" />
  </ItemGroup>

</Project>
</pre>

<pre class="file" data-filename="Program.cs" data-target="replace">
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
</pre>
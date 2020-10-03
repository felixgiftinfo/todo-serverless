using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Todo_API.Services;

[assembly: FunctionsStartup(typeof(Todo_API.Startup))]
namespace Todo_API
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; private set; }
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddLogging(x =>
            {
                x.AddFilter(level => true);
            });


            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new Helper.AutomapperProfile()));
            builder.Services.AddSingleton(mapperConfiguration.CreateMapper());
             Configuration = (IConfiguration)builder.Services.First(d => d.ServiceType == typeof(IConfiguration)).ImplementationInstance;


            //var localRoot = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");
            //var azureRoot = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";

            //var actualRoot = localRoot ?? azureRoot;

            //var configBuilder = new ConfigurationBuilder()
            //    .SetBasePath(actualRoot)
            //    .AddEnvironmentVariables()
            //    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
            //Configuration = configBuilder.Build();

            builder.Services.AddSingleton<IConfiguration>(Configuration);

            builder.Services.AddSingleton<ITodoService, TodoService_Mongo>();
        }

    }
}

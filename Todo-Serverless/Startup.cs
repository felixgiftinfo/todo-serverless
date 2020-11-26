
using AutoMapper;
using MediatR;
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
using Todo_Serverless.Services;

[assembly: FunctionsStartup(typeof(Todo_Serverless.Startup))]
namespace Todo_Serverless
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
            // Configuration = (IConfiguration)builder.Services.First(d => d.ServiceType == typeof(IConfiguration)).ImplementationInstance;


            builder.Services.AddMediatR(typeof(Startup));

            builder.Services.AddSingleton<ITodoService, TodoService_Mongo>();
            builder.Services.AddSingleton<ITodoAPIService, TodoAPIService>();

        }

    }
}

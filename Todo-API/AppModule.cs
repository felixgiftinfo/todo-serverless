using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using Module = Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions.Module;

namespace Todo_API
{
    public class AppModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            base.Load(services);
            
            services.AddAutoMapper(Assembly.GetAssembly(this.GetType()));
        }
    }
}

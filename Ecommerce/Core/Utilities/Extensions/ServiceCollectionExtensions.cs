﻿
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, IConfiguration configuration,ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services,configuration);
            }
            return ServiceTool.Create(services);
        }
    }
}

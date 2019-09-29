using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mahogany
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add external projects into the main asp.net core project
        /// </summary>        
        public static void AddProjects(this IServiceCollection services, IMvcBuilder mvcBuilder, IList<Assembly> assemblies)
        {
            foreach(var assembly in assemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);

                services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.FileProviders.Add(new EmbeddedFileProvider(assembly));
                });
            }
        }
    }
}

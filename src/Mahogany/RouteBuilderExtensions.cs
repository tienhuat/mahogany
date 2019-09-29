using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Mahogany
{     
    public static class RouteBuilderExtensions
    {        
        public static void MapMahoganyRoute(this IRouteBuilder routes, IServiceProvider serviceProvider, IList<Assembly> assemblies)
        {
            foreach(var assembly in assemblies)
            {
                var typeDiscoverer = new ServiceLocator();
                var action = typeDiscoverer.GetMapRouteAction(assembly);
                action.Execute(routes, serviceProvider);
            }
        }
    }
}

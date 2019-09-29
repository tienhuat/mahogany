using Mahogany;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project2
{
    public class MapRouteAction : IMapRouteAction
    {
        public void Execute(IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapRoute(
                  name: "project2.BoundedContext2",
                  template: "{area:exists}/{controller}/{action}/{id?}",
                  defaults: new { controller = "Home", action = "Index" },
                  constraints: new { area = "BoundedContext2" }
                );

            routes.MapRoute(
                 name: "project2.BoundedContext3",
                 template: "{area:exists}/{controller}/{action}/{id?}",
                 defaults: new { controller = "Home", action = "Index" },
                 constraints: new { area = "BoundedContext3" }
               );
        }            
    }
}

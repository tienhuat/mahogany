using Mahogany;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project1
{
    public class MapRouteAction : IMapRouteAction
    {
        public void Execute(IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapRoute(
                   name: "project1.BoundedContext1",
                   template: "{area:exists}/{controller}/{action}/{id?}",
                   defaults: new { controller = "Home", action = "Index" }                   
                   ,constraints: new { area = "BoundedContext1" }
                 );            
        }            
    }
}

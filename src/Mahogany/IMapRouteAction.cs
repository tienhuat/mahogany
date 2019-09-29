using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mahogany
{
    
    public interface IMapRouteAction
    {   
        void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider);
    }
}

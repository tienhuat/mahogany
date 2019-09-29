using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project1.BoundedContexts.BoundedContext1.Features.FeatureGrouping1.Hello
{    
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

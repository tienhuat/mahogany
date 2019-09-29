using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace project2.BoundedContexts.BoundedContext3.Features.Hello
{       
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
 
}

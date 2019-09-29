using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace project2.BoundedContexts.BoundedContext3.Features.Home
{    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
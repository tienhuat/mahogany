using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace Mahogany
{
    public static class FeatureRazorExtensions
    {
        public static void ConfigureFeatureFolders(this RazorViewEngineOptions options)
        {
            // {0} - Action Name
            // {1} - Controller Name
            // {2} - Area name (= Bounded Context Name)
            // {3} - Feature Name
            // <4> - one level below feature
            // <5> - two levels below feature
            // <6> - three levels below feature

            // Replace the area view location entirely
             options.AreaViewLocationFormats.Clear();

            // Search the view up to three levels below feature
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/<6>/<5>/<4>/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/<6>/<5>/<4>/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/<5>/<4>/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/<5>/<4>/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/<4>/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/<4>/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/{2}/Features/Shared/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/BoundedContexts/Features/Shared/{0}.cshtml");

            options.AreaViewLocationFormats.AddDistinct("/Features/<6>/<5>/<4>/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/<6>/<5>/<4>/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/<5>/<4>/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/<5>/<4>/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/<4>/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/<4>/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/{3}/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/{3}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/{0}.cshtml");
            options.AreaViewLocationFormats.AddDistinct("/Features/Shared/{0}.cshtml");
                       
            // Replace normal view location entirely
            options.ViewLocationFormats.Clear();          
            
            // Search the view up to three levels below feature
            options.ViewLocationFormats.AddDistinct("/Features/<6>/<5>/<4>/{3}/{1}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/<6>/<5>/<4>/{3}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/<5>/<4>/{3}/{1}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/<5>/<4>/{3}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/<4>/{3}/{1}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/<4>/{3}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/{3}/{1}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/{3}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/{1}/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/{0}.cshtml");
            options.ViewLocationFormats.AddDistinct("/Features/Shared/{0}.cshtml");

            options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
        }
                
        private static void AddDistinct(this IList<string> list, string item)
        {
            if(!list.Where(i => i == item).Any())
            {
                list.Add(item);
            }
        }
    }
}

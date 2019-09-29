using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mahogany
{
    public class PageRouteConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            foreach (var selector in model.Selectors)
            {
                var template = selector.AttributeRouteModel.Template;

                // Sample template for a page:
                // BoundedContexts.BoundedContext3.Features.Hello.Visitor
               
                // The feature is one level above the Page class
                var featureName = GetFeatureName(template);
                var boundedContextName = GetBoundedContextName(template);
                var pageName = GetPageName(template);

                if(!string.IsNullOrEmpty(featureName))
                {
                    if(!string.IsNullOrEmpty(boundedContextName))
                    {
                        selector.AttributeRouteModel.Template = $"{boundedContextName}/{featureName}/{pageName}";                        
                    }
                    else
                    {
                        selector.AttributeRouteModel.Template = $"{featureName}/{pageName}";
                    }
                }
            }
        }


        private string GetPageName(string template)
        {
            string[] tokens = SplitTemplate(template);
            return tokens.Last();
        }

        private string GetFeatureName(string template)
        {
            string[] tokens = SplitTemplate(template);
            if (!tokens.Any(t => t == "Features"))
            {
                return "";
            }

            // The feature is one level above the Page class
            string featureName = tokens
               .Reverse()
               .Skip(1)
               .FirstOrDefault();

            return featureName;
        }

        private string GetBoundedContextName(string template)
        {
            string[] tokens = SplitTemplate(template);
            if (!tokens.Any(t => t == "BoundedContexts"))
            {
                return "";
            }

            // The bounded context is one level below the BoundedContexts namespace         
            string boundedContextName = tokens
                 .SkipWhile(t => t != "BoundedContexts")
                 .Skip(1)                
                 .FirstOrDefault();

            return boundedContextName;
        }

        private string[] SplitTemplate(string template)
        {            
            var hasSlash = template.IndexOf("/") >= 0;

            if(hasSlash)
            {
                return template.Split('/');
            }

            return template.Split('.');
        }
    }
}

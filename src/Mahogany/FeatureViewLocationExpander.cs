using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mahogany
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            var pageActionDescription = context.ActionContext.ActionDescriptor as PageActionDescriptor;
            

            if (controllerActionDescriptor == null)
            {
                throw new NullReferenceException("ControllerActionDescriptor cannot be null.");
            }

            string featureName = controllerActionDescriptor.Properties["feature"] as string;
            string boundedContextName = controllerActionDescriptor.Properties["area"] as string;
            foreach (var location in viewLocations)
            {
                var locationFilled = location
                    .Replace("{3}", featureName)
                    .Replace("{2}", boundedContextName);

                // Fill up the super groups of the feature
                int groupIndex = 4;
                string groupKey = "<" + groupIndex + ">";
                while (controllerActionDescriptor.Properties.ContainsKey(groupKey))
                {
                    var group = controllerActionDescriptor.Properties[groupKey] as string;
                    locationFilled = locationFilled.Replace(groupKey, group);
                    groupIndex++;
                    groupKey = "<" + groupIndex + ">";
                }

                yield return locationFilled;
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}

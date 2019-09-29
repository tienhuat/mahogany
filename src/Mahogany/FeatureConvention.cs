using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mahogany
{
    public class FeatureConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // {0} - Action Name
            // {1} - Controller Name
            // {2} - Area Name (= Bounded Context Name)
            // {3} - Feature Name
            // <4> - one level below feature
            // <5> - two levels below feature
            // <6> - three levels below feature

            var featureName = GetFeatureName(controller.ControllerType);
            controller.Properties.Add("feature", featureName);
            
            var featureSuperGroups = GetFeatureSuperGroups(controller.ControllerType);
            int groupIndex = 4;
            foreach (var group in featureSuperGroups)
            {
                var groupKey = "<" + groupIndex + ">";
                controller.Properties.Add(groupKey, group);
                groupIndex++;
            }

            var boundedContextName = GetBoundedContextName(controller.ControllerType);
            controller.Properties.Add("area", boundedContextName);
            
            if (boundedContextName != "")
            {                
                controller.RouteValues.Add("area", boundedContextName);

                // For API
                var _routePrefix = new AttributeRouteModel(new RouteAttribute(boundedContextName));
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel != null)
                    {
                        selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                    }                   
                }
            }
        }

        private string GetFeatureName(TypeInfo controllerType)
        {            
            string[] tokens = controllerType.FullName.Split('.');
            if (!tokens.Any(t => t == "Features"))
            {
                return "";
            }

            // The feature is one level above the Controller class
            string featureName = tokens
                .TakeWhile(t => t.IndexOf("Controller") < 0)
                .Reverse()                
                .Take(1)
                .FirstOrDefault();
            
            return featureName;
        }
               
        private string GetBoundedContextName(TypeInfo controllerType)
        {
            string[] tokens = controllerType.FullName.Split('.');
            if (!tokens.Any(t => t == "BoundedContexts"))
            {
                return "";
            }

            // The bounded context is one level below the BoundedContexts namespace         
            string boundedContextName = tokens
                 .SkipWhile(t => t != "BoundedContexts")
                 .Skip(1)
                 .Take(1)
                 .FirstOrDefault();

            return boundedContextName;
        }

        /// <summary>
        /// Get the super groups of a features.
        /// The super groups are below Features namespace and above Controller class. 
        /// Eg. for project1.BoundedContexts.BoundedContext1.Features.SuperSuperGroup.SuperGroup.HelloController, 
        /// the super groups are [SuperGroup, SuperSuperGroup]
        /// </summary>
        private IList<string> GetFeatureSuperGroups(TypeInfo controllerType)
        {
            string[] tokens = controllerType.FullName.Split('.');
            if (!tokens.Any(t => t == "Features"))
            {
                return new List<string>();
            }

            var sssk = tokens
               .SkipWhile(t => !t.Equals("Features"))
               .Skip(1)
               .TakeWhile(t => !t.Contains("Controller"));

            var superGroups = tokens
               .SkipWhile(t => !t.Equals("Features"))
               .Skip(1)
               .TakeWhile(t => !t.Contains("Controller"))
               .Reverse()
               .Skip(1)
               .ToList();

            return superGroups;
        }
    }
}

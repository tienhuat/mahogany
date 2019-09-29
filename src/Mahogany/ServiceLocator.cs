using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace Mahogany
{
    class ServiceLocator
    {

        public IMapRouteAction GetMapRouteAction(Assembly assembly)
        {
            return GetInstance<IMapRouteAction>(assembly);
        }

        private static T GetInstance<T>(Assembly assembly, params object[] args)
        {
            List<T> instances = new List<T>();

            foreach (Type implementation in GetImplementations<T>(assembly))
            {
                if (!implementation.GetTypeInfo().IsAbstract)
                {
                    T instance = (T)Activator.CreateInstance(implementation, args);

                    instances.Add(instance);
                }
            }

            return instances.First();
        }

        private static IEnumerable<Type> GetImplementations<T>(Assembly assembly)
        {
            Type type = typeof(T);
            List<Type> implementations = new List<Type>();

            foreach (Type exportedType in assembly.GetExportedTypes())
            {
                if (type.GetTypeInfo().IsAssignableFrom(exportedType) && exportedType.GetTypeInfo().IsClass)
                {
                    implementations.Add(exportedType);
                }
            }

            return implementations;
        }

    }
}

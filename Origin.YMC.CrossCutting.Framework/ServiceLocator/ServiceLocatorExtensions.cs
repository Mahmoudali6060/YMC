using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.CrossCutting.Framework.ServiceLocator
{
    public static class ServiceLocatorExtensions
    {
        public static TService GetService<TService>(this IServiceLocator serviceLocator)
        {
            return (TService)serviceLocator.GetService(typeof(TService));
        }

        public static IEnumerable<TService> GetServices<TService>(this IServiceLocator serviceLocator)
        {
            return serviceLocator.GetServices(typeof(TService)).Cast<TService>();
        }
    }
}

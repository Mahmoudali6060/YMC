using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Origin.YMC.CrossCutting.Framework.ServiceLocator
{
    public class MvcServiceLocator : IServiceLocator
    {
        private readonly IDependencyResolver _dependencyResolver;

        public MvcServiceLocator(IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");
            _dependencyResolver = dependencyResolver;
        }

        public object GetService(Type serviceType)
        {
            return _dependencyResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _dependencyResolver.GetServices(serviceType);
        }
    }
}

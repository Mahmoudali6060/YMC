using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Origin.YMC.CrossCutting.Framework.ServiceLocator
{
    public class MvcServiceLocatorFactory : IServiceLocatorFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        public MvcServiceLocatorFactory(IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");
            _dependencyResolver = dependencyResolver;
        }

        #region IServiceLocatorFactory Members

        public IServiceLocator Create()
        {
            return new MvcServiceLocator(_dependencyResolver);
        }

        #endregion
    }
}

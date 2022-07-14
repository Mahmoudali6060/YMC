using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.CrossCutting.Framework.ServiceLocator
{
    public interface IServiceLocator
    {
        /// <summary>
        /// Resolves registered service.
        /// </summary>
        /// 
        /// <returns>
        /// The requested service or object.
        /// </returns>
        /// <param name="serviceType">The type of the requested service or object.</param>
        object GetService(Type serviceType);

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// 
        /// <returns>
        /// The requested services.
        /// </returns>
        /// <param name="serviceType">The type of the requested services.</param>
        IEnumerable<object> GetServices(Type serviceType);
    }
}

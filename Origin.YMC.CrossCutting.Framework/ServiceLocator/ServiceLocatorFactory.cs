using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.CrossCutting.Framework.ServiceLocator
{
    /// <summary>
    /// ServiceLocatorFactory creates ServiceLocator
    /// </summary>
    public static class ServiceLocatorFactory
    {
        #region Members

        private static IServiceLocatorFactory _currentFactory = null;
        #endregion

        #region public members

        public static IServiceLocatorFactory CurrentFactory { get { return _currentFactory; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the current.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <exception cref="System.ArgumentNullException">factory</exception>
        public static void SetCurrent(IServiceLocatorFactory factory)
        {
            if (null == factory)
                throw new ArgumentNullException("factory");
            _currentFactory = factory;
        }

        /// <summary>
        /// Creates the service locator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Factory is null, You must first set current factory.</exception>
        public static IServiceLocator CreateServiceLocator()
        {
            if (null == CurrentFactory)
                throw new InvalidOperationException("Factory is null, You must first set current factory.");
            return CurrentFactory.Create();
        }

        #endregion
    }
}

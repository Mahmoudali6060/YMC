using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.CrossCutting.Framework
{
    public static class IdentityProviderFactory
    {
        #region Members


        private static IIdentityProviderFactory _currentIdentityFactory;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current Identity factory.
        /// </summary>
        /// <value>
        /// The current Identity factory.
        /// </value>
        public static IIdentityProviderFactory CurrentIdentityFactory { get { return _currentIdentityFactory; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the current.
        /// </summary>
        /// <param name="identityFactory">The identity factory.</param>
        /// <exception cref="System.ArgumentNullException">IdentityFactory</exception>
        public static void SetCurrent(IIdentityProviderFactory identityFactory)
        {

            if (null == identityFactory)
                throw new ArgumentNullException("identityFactory");
            _currentIdentityFactory = identityFactory;
        }

        /// <summary>
        /// Creates the identity provider.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">CurrentIdentityFactory is null, You must first set current Identityger factory.</exception>
        public static IIdentityProvider CreateIdentity()
        {
            if (null == CurrentIdentityFactory)
                throw new InvalidOperationException("CurrentIdentityFactory is null, You must first set current identity factory.");
            return CurrentIdentityFactory.Create();
        }
        #endregion
    }
}

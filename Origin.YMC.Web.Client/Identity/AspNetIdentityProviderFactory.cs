using Autofac.Integration.Mvc;
using Origin.YMC.Business.Components.Interfaces;
using Origin.YMC.CrossCutting.Framework;
using Origin.YMC.CrossCutting.Framework.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Origin.YMC.Web.Client.Identity
{
    public class AspNetIdentityProviderFactory : IIdentityProviderFactory
    {
        #region IIdentityProviderFactory Members

        public AspNetIdentityProviderFactory()
        {
        }

        public IIdentityProvider Create()
        {
            
            //use cross cutting service to get request level service
            if (HttpContext.Current != null)
                return new AspNetIdentityProvider(HttpContext.Current.User, ServiceLocatorFactory.CreateServiceLocator().GetService<IUserComponent>());

            return new AspNetIdentityProvider(Thread.CurrentPrincipal, ServiceLocatorFactory.CreateServiceLocator().GetService<IUserComponent>());
        }

        #endregion
    }
}
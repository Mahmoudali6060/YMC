using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Origin.YMC.Repositories;
using Origin.YMC.Business.Components.Implementation;
using Origin.YMC.Business.Components.Interfaces;
using System.Reflection;
using Origin.YMC.Web.Services.Api.Identity;
using Origin.YMC.CrossCutting.Framework;
using Origin.YMC.CrossCutting.Framework.ServiceLocator;
using System.Configuration;
using Autofac.Integration.WebApi;

namespace Origin.YMC.Web.Services.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterAutofacDependancies();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
            new Origin.YMC.Business.Entities.DbInitializer().Init();
        }
        #region IoC
        private void RegisterAutofacDependancies()
        {
            var builder = new ContainerBuilder();

            //Register all the types in the module...

            // register the global maindb's connection string
            //builder.RegisterInstance(ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString)
            //    .Named<string>("connectionStringName");

            builder.RegisterType<SqlUnitOfWork>()
                 .As<IQueryableUnitOfWork>()
                 .InstancePerRequest();

            builder.RegisterType<SqlUnitOfWork>()
                .As<ISqlCommand>()
                .InstancePerRequest();

            builder.RegisterType<SqlUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>))
                  .As(typeof(IRepository<>))
                  .InstancePerRequest();

            builder.RegisterType<UserComponent>()
                  .As<IUserComponent>()
                  .InstancePerRequest();

            builder.RegisterType<LogComponent>()
                   .As<ILogComponent>()
                   .InstancePerRequest();

            builder.RegisterType<CityComponent>()
                   .As<ICityComponent>()
                   .InstancePerRequest();

            builder.RegisterType<AttachmentComponent>()
                   .As<IAttachmentComponent>()
                   .InstancePerRequest();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());


            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        #endregion
    }
}

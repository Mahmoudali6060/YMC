using Autofac;
using Autofac.Integration.Mvc;
using Origin.YMC.Repositories;
using Origin.YMC.Business.Components.Implementation;
using Origin.YMC.Business.Components.Interfaces;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Origin.YMC.Web.Client.Identity;
using Origin.YMC.CrossCutting.Framework;
using Origin.YMC.CrossCutting.Framework.ServiceLocator;
using System.Configuration;
using Origin.YMC.Web.Client.Services;
using Microsoft.AspNet.Identity;

namespace Origin.YMC.Web.Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterAutofacDependancies();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
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

            builder.RegisterType<CountryComponent>()
                   .As<ICountryComponent>()
                   .InstancePerRequest();

            builder.RegisterType<PatientComponent>()
                   .As<IPatientComponent>()
                   .InstancePerRequest();

            builder.RegisterType<DoctorComponent>()
                   .As<IDoctorComponent>()
                   .InstancePerRequest();

            builder.RegisterType<PaymentComponent>()
                   .As<IPaymentComponent>()
                   .InstancePerRequest();

            builder.RegisterType<CityComponent>()
                   .As<ICityComponent>()
                   .InstancePerRequest();


            builder.RegisterType<AttachmentComponent>()
                   .As<IAttachmentComponent>()
                   .InstancePerRequest();

            builder.RegisterType<PartationComponent>()
                   .As<IPartationComponent>()
                   .InstancePerRequest();


            builder.RegisterType<PartationTypeComponent>()
                   .As<IPartationTypeComponent>()
                   .InstancePerRequest();

            builder.RegisterType<SocialComponent>()
                   .As<ISocialComponent>()
                   .InstancePerRequest();

            builder.RegisterType<TestimonialComponent>()
                   .As<ITestimonialComponent>()
                   .InstancePerRequest();

            builder.RegisterType<SpecialtyComponent>()
                   .As<ISpecialtyComponent>()
                   .InstancePerRequest();

            builder.RegisterType<CaseQuestionsComponent>()
                   .As<ICaseQuestionsComponent>()
                   .InstancePerRequest();

            builder.RegisterType<EmailService>()
                   .As<IIdentityMessageService>()
                   .InstancePerRequest();
            
            builder.RegisterType<CaseComponent>()
                   .As<ICaseComponent>()
                   .InstancePerRequest();

            builder.RegisterType<QuestionThreadComponent>()
                   .As<IQuestionThreadComponent>()
                   .InstancePerRequest();

            builder.RegisterType<StatisticsComponent>()
                   .As<IStatisticsComponent>()
                   .InstancePerRequest();
            builder.RegisterType<InterpreterComponent>()
                .As<IInterpreterComponent>()
                .InstancePerRequest();
            builder.RegisterType<PatientCallRequestsComponent>()
                .As<IPatientCallRequestsComponent>()
                .InstancePerRequest();
            

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            //Set factories ...
            IdentityProviderFactory.SetCurrent(new AspNetIdentityProviderFactory());
            ServiceLocatorFactory.SetCurrent(new MvcServiceLocatorFactory(resolver));
            DependencyResolver.SetResolver(resolver);
        }
        #endregion
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Origin.YMC.Web.Client.Startup))]
namespace Origin.YMC.Web.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

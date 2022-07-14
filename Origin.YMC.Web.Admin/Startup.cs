using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Origin.YMC.Web.Admin.Startup))]
namespace Origin.YMC.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

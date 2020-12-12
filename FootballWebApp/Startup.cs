using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballWebApp.Startup))]
namespace FootballWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoliceServer.Startup))]
namespace PoliceServer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }

    }
}

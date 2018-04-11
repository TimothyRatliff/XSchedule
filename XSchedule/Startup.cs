using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XSchedule.Startup))]
namespace XSchedule
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NerdNight.Startup))]
namespace NerdNight
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

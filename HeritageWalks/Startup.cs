using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeritageWalks.Startup))]
namespace HeritageWalks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

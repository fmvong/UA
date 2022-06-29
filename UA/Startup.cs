using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UA.Startup))]
namespace UA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

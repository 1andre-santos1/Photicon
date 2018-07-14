using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Photicon.Startup))]
namespace Photicon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

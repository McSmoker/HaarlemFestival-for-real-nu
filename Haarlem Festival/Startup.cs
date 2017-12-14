using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Haarlem_Festival.Startup))]
namespace Haarlem_Festival
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

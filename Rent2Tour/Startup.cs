using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rent2Tour.Startup))]
namespace Rent2Tour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetMVC5Demo.Startup))]
namespace DotNetMVC5Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

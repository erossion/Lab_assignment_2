using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab_10.Startup))]
namespace Lab_10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

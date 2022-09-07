using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agenda2022.Startup))]
namespace Agenda2022
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

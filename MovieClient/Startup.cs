using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieClient.Startup))]
namespace MovieClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

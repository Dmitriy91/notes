using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Notes.Web.Startup))]
namespace Notes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

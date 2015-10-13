using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthPoc.Web.Startup))]
namespace AuthPoc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginCRUD.Startup))]
namespace LoginCRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

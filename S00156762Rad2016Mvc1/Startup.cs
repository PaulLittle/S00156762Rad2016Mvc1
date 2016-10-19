using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S00156762Rad2016Mvc1.Startup))]
namespace S00156762Rad2016Mvc1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FeedBackDemo.Startup))]
namespace FeedBackDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

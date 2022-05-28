using Microsoft.Owin;
using Owin;
using WebDiary;

[assembly: OwinStartupAttribute(typeof(Startup))]

namespace WebDiary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
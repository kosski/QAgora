using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QAgoraForum.Startup))]
namespace QAgoraForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

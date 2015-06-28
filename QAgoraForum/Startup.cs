using Microsoft.Owin;
using Owin;
using QAgoraForum.App_Start;

[assembly: OwinStartupAttribute(typeof(QAgoraForum.Startup))]
namespace QAgoraForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request

            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            ConfigureAuth(app);
        }
    }
}


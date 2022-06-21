using Microsoft.Owin;
using NewsNowProject.WebUI.Jobs.Scheduler;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsNowProject.WebUI.Startup))]
namespace NewsNowProject.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();

            GetUpdateNewsScheduler.Start();
            //ParserScheduler.Start();
            //ProxyListScheduler.Start();
        }
    }
}

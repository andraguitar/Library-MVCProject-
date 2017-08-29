using System;
using Duke.Owin.VkontakteMiddleware;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Nemiro.OAuth;
using Nemiro.OAuth.Clients;


namespace WebApplication3
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "Library",
                    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.MapSignalR();
            
            // these lines of code configure the various providers we want to use
            app.UseVkontakteAuthentication(new VkAuthenticationOptions()
            {
                AppId = "6143696",
                AppSecret = "uhncc3U7N5SCtEQRtHrv"
            });
        }
    }
}
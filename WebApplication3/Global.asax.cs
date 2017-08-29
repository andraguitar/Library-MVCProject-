using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Nemiro.OAuth;
using Nemiro.OAuth.Clients;
using Ninject;
using WebApplication3.Infrastructure;
using WebApplication3.Mapping;

namespace WebApplication3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(x => x.AddProfile<MapProfile>());
            IKernel kernel = new StandardKernel();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            OAuthManager.RegisterClient
            (
                new VkontakteClient
                (
                    "6143696",
                    "uhncc3U7N5SCtEQRtHrv"
                )
            );
        }
    }
}

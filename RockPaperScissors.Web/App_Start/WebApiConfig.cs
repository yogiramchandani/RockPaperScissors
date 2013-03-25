using System.Web.Http;

namespace RockPaperScissors.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Games", id = RouteParameter.Optional }
            );
        }
    }
}

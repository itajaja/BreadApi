using System.Web.Http;

namespace Hylasoft.BreadApi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
//      config.Routes.MapHttpRoute(
//          name: "DefaultApi",
//          routeTemplate: "api/{controller}/{id}",
//          defaults: new { id = RouteParameter.Optional }
//      );

      config.Routes.MapHttpRoute(
            name: "BreadApi",
            routeTemplate: "breads/{bread}/{breadClass}/{method}",
            defaults: new { id = RouteParameter.Optional, controller = "Bread", action = "Invoke" }
        );
    }
  }
}

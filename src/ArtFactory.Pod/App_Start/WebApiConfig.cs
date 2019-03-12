namespace ArtFactory.Pod
{
  using Newtonsoft.Json.Serialization;
  using System.Web.Http;

  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      config.Formatters.Remove(config.Formatters.XmlFormatter);
      var jsonFormatter = config.Formatters.JsonFormatter;

      jsonFormatter.SerializerSettings.ContractResolver 
        = new CamelCasePropertyNamesContractResolver();

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}

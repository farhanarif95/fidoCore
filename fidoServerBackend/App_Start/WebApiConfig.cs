using System.Web.Http;

namespace fidoServerBackend
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "tables/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}

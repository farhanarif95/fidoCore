using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(fidoServer.Startup))]
namespace fidoServer
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureMobileApp(app);
		}
	}
}

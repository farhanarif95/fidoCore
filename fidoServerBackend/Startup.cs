using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(fidoServerBackend.Startup))]
namespace fidoServerBackend
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
				ConfigureMobileApp(app);
		}
	}
}

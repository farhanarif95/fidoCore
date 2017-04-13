using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using fidoServerBackend.DataObjects;
using fidoServerBackend.Models;
using Owin;

namespace fidoServerBackend
{
	public partial class Startup
	{
		public static void ConfigureMobileApp(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			var mobileConfig = new MobileAppConfiguration();

			mobileConfig
				.AddTablesWithEntityFramework()
				.ApplyTo(config);

			Database.SetInitializer(new MobileServiceInitializer());

			app.UseWebApi(config);
		}
	}

	public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
	{
		protected override void Seed(MobileServiceContext context)
		{
			List<Users> users = new List<Users>()
			{
				new Users(){
					Id=Guid.NewGuid().ToString(), name="Abdul Muhaymin Arif", email="mampary@hotmail.com", organisation="FantaCode", password="naseef", roles="All"}
			};

			foreach (var user in users)
			{
				context.Set<Users>().Add(user);
			}

			base.Seed(context);
		}
	}
}
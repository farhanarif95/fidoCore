using System;
using Microsoft.Azure.Mobile.Server;

namespace fidoServer
{
	public class Users : EntityData
	{
		public string email { get; set; }
		public string name { get; set; }
		public string password { get; set; }
		public string organisation { get; set; }
		public string roles { get; set; }
	}
}

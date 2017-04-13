using System;
using Microsoft.Azure.Mobile.Server;

namespace fidoServerBackend.DataObjects
{
	public class Organisations : EntityData
	{
			public string id { get; set; }
			public string companyName { get; set; }
			public string address { get; set; }
	}
}

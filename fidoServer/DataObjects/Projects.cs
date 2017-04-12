using System;
using Microsoft.Azure.Mobile.Server;

namespace fidoServer.DataObjects
{
	public class Projects : EntityData
	{
		public string customerName { get; set; }
		public string customerEmail { get; set; }
		public string customerAddress { get; set; }
		public DateTimeOffset? startDate { get; set; }
		public DateTimeOffset? endDate { get; set; }
		public string leaders { get; set; }
	}
}

using System;
using Microsoft.Azure.Mobile.Server;

namespace fidoServer.DataObjects
{
	public class Tasks : EntityData
	{
		public string projectId { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public string assignedTo { get; set; }
		public string status { get; set; }
		public DateTimeOffset? start { get; set; }
		public DateTimeOffset? end { get; set; }
	}
}

using System;
using Microsoft.Azure.Mobile.Server;

namespace fidoServerBackend.DataObjects
{
	public class Teams : EntityData
	{
		public string userId { get; set; }
		public string projectId { get; set; }
	}
}

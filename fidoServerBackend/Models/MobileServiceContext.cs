using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using fidoServerBackend.DataObjects;

namespace fidoServerBackend.Models
{
	public class MobileServiceContext : DbContext
	{
		private const string connectionStringName = "Name=MS_TableConnectionString";

		public MobileServiceContext() : base(connectionStringName)
		{
		}

		public DbSet<Projects> Projects { get; set; }
		public DbSet<Tasks> Tasks { get; set; }
		public DbSet<Teams> Teams { get; set; }
		public DbSet<Users> Users { get; set; }
		public DbSet<Organisations> Organisations { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Add(
				new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
					"ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
		}
	}
}
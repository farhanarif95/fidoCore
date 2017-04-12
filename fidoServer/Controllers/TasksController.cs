using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using fidoServer.DataObjects;
using fidoServer.Models;
using Microsoft.Azure.Mobile.Server;

namespace fidoServer.Controllers
{
	public class TasksController : TableController<Tasks>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Tasks>(context, Request);
		}

		// GET tables/Tasks
		public IQueryable<Tasks> GetAllTaskss() => Query();

		// GET tables/Tasks/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Tasks> GetTasks(string id) => Lookup(id);

		// PATCH tables/Tasks/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Tasks> PatchTasks(string id, Delta<Tasks> patch) => UpdateAsync(id, patch);

		// POST tables/Tasks
		public async Task<IHttpActionResult> PostTasks(Tasks item)
		{
			Tasks current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/Tasks/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteTasks(string id) => DeleteAsync(id);
	}
}
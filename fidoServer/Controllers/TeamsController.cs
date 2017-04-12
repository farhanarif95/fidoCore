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
	public class ProjectsController : TableController<Projects>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Projects>(context, Request);
		}

		// GET tables/Projects
		public IQueryable<Projects> GetAllProjectss() => Query();

		// GET tables/Projects/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Projects> GetProjects(string id) => Lookup(id);

		// PATCH tables/Projects/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Projects> PatchProjects(string id, Delta<Projects> patch) => UpdateAsync(id, patch);

		// POST tables/Projects
		public async Task<IHttpActionResult> PostProjects(Projects item)
		{
			Projects current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/Projects/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteProjects(string id) => DeleteAsync(id);
	}
}
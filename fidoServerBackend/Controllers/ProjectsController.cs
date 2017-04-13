using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using fidoServerBackend.DataObjects;
using fidoServerBackend.Models;
using Microsoft.Azure.Mobile.Server;

namespace fidoServerBackend.Controllers
{
	public class TeamsController : TableController<Teams>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Teams>(context, Request);
		}

		// GET tables/Teams
		public IQueryable<Teams> GetAllTeamss() => Query();

		// GET tables/Teams/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Teams> GetTeams(string id) => Lookup(id);

		// PATCH tables/Teams/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Teams> PatchTeams(string id, Delta<Teams> patch) => UpdateAsync(id, patch);

		// POST tables/Teams
		public async Task<IHttpActionResult> PostTeams(Teams item)
		{
			Teams current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/Teams/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteTeams(string id) => DeleteAsync(id);
	}
}
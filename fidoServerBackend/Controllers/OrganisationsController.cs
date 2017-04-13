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
	public class OrganisationsController : TableController<Organisations>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Organisations>(context, Request);
		}

		// GET tables/Organisations
		public IQueryable<Organisations> GetAllOrganisationss() => Query();

		// GET tables/Organisations/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Organisations> GetOrganisations(string id) => Lookup(id);

		// PATCH tables/Organisations/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Organisations> PatchOrganisations(string id, Delta<Organisations> patch) => UpdateAsync(id, patch);

		// POST tables/Organisations
		public async Task<IHttpActionResult> PostOrganisations(Organisations item)
		{
			Organisations current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/Organisations/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteOrganisations(string id) => DeleteAsync(id);
	}
}
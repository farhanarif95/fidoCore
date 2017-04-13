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
	public class UsersController : TableController<Users>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Users>(context, Request);
		}

		// GET tables/Users
		public IQueryable<Users> GetAllUsers() => Query();

		// GET tables/Users/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Users> GetUsers(string id) => Lookup(id);

		// PATCH tables/Users/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Users> PatchUsers(string id, Delta<Users> patch) => UpdateAsync(id, patch);

		// POST tables/Users
		public async Task<IHttpActionResult> PostUsers(Users item)
		{
			Users current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/Users/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteUsers(string id) => DeleteAsync(id);
	}
}
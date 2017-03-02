using fidoBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Services
{
    public class OrganisationServices : BaseService
    {
        public async static Task<Status> Signup(Organisation org)
        {
            try
            {
                await MobileService.GetTable<Organisation>().InsertAsync(org);
                return new Models.Status() { result = true, message = "Successfully Registered" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }

        }
    }
}

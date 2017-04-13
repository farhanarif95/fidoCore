using fidoBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fidoBackend.Services
{
    public class ProjectServices : BaseService
    {
        public static async Task ListProjectsAsync(string userId)
        {
            var usersregisteredprojects = await MobileService.GetTable<Teams>().Where(x => x.userId == userId).ToListAsync();
            var allprojects = await


        }
    }
}

using fidoBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Services
{
    public class UserServices : BaseService
    {
        public async static Task<Status> Signup(Users user)
        {
            try
            {
                var users = await MobileService.GetTable<Users>().Where(x => x.email == user.email).ToListAsync();
                if (users.FirstOrDefault() == null)
                {
                    await MobileService.GetTable<Users>().InsertAsync(user);
                    return new Models.Status() { result = true, message = "Successfully Registered" };
                }
                else
                {
                    return new Models.Status() { result = false, message = "Already Registererd" };
                }
            }
            catch(Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }
    }
}

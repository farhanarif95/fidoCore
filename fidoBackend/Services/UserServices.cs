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

        public async static Task<Status> SignIn(string username, string password)
        {
            try
            {
                var users = await MobileService.GetTable<Users>().Where(x => x.email == username).ToListAsync();
                var selected = users.FirstOrDefault();
                if (selected!=null)
                {
                    if(selected.password.Equals(password))
                    {
                        return new Models.Status() { result = true, message = "Successfully Logged In",data=selected };
                    }
                    else
                    {
                        return new Models.Status() { result = false, message = "Invalid Password" };
                    }
                }
                else
                {
                    return new Models.Status() { result = false, message = "No account found!" };
                }
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }
    }
}

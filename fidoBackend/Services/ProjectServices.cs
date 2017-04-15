using fidoBackend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fidoBackend.Services
{
    public class ProjectServices : BaseService
    {
        
        public async static Task<Status> AddProject(Projects project, string userid)
        {
            try
            {
                await MobileService.GetTable<Projects>().InsertAsync(project);
                var team = new Teams();
                team.projectId = project.id;
                team.userId = userid;
                await MobileService.GetTable<Teams>().InsertAsync(team);
                return new Models.Status() { result = true, message = "Successfully Added" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }
        public static async Task<Status> ListProjects(string userId)
        {
            var s = "listProjects/?userId=" + userId;
            var res = await BaseService.HttpGetOperation(s);
            if (res != null)
            {

                var dat = JsonConvert.DeserializeObject<List<Projects>>(res.data.ToString());
                return new Models.Status() { result = true, message = "Successfully loaded", data=dat }; ;
            }
            else
            {
                return new Models.Status() { result = false, message = "Error, cannot load projects" };
            }
        }

        public static async Task<Status> ListTasks(string userId, string projectId)
        {
            var s = "listTasks/?userId=" + userId + "&" + "projectId=" + projectId;
            var res = await BaseService.HttpGetOperation(s);
            if (res != null)
            {

                var dat = JsonConvert.DeserializeObject<List<Tasks>>(res.data.ToString());
                return new Models.Status() { result = true, message = "Successfully loaded", data = dat }; ;
            }
            else
            {
                return new Models.Status() { result = false, message = "Error, cannot load projects" };
            }
        }
        public static async Task<Status> ListAssignedTasksinProject(string userId, string projectId)
        {
            var s = "listAssignedTasksInProject/?userId=" + userId + "&" + "projectId=" + projectId;
            var res = await BaseService.HttpGetOperation(s);
            if (res != null)
            {

                var dat = JsonConvert.DeserializeObject<List<Tasks>>(res.data.ToString());
                return new Models.Status() { result = true, message = "Successfully loaded", data = dat }; ;
            }
            else
            {
                return new Models.Status() { result = false, message = "Error, cannot load projects" };
            }
        }

        public async static Task<Status> UpdateTeamInProject(List<Users> users, Temp projectId)
        {
            try
            {
                var res = await GetTeamInProject(projectId.projectId);
                if (res.result)
                {
                    var selectedTeam = res.data as List<Teams>;
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].member)
                        {
                            var temo = await BaseService.MobileService.GetTable<Teams>().Where(x => x.userId == users[i].id).ToListAsync();
                            var item = temo.FirstOrDefault();
                            if (item == null)
                            {
                                var team = new Teams();
                                team.projectId = projectId.projectId;
                                team.userId = users[i].id;
                                await BaseService.MobileService.GetTable<Teams>().InsertAsync(team);
                            }
                        }
                        else
                        {
                            var temo = await BaseService.MobileService.GetTable<Teams>().Where(x => x.userId == users[i].id).ToListAsync();
                            var item = temo.FirstOrDefault();
                            if (item != null)
                            {
                                await BaseService.MobileService.GetTable<Teams>().DeleteAsync(item);
                            }
                        }
                    }
                    return new Models.Status() { result = true, message = "Success" };
                }
                else
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        var team = new Teams();
                        team.projectId = projectId.projectId;
                        team.userId = users[i].id;
                        await BaseService.MobileService.GetTable<Teams>().InsertAsync(team);
                    }
                    return new Models.Status() { result =true, message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }

        public async static Task<Status> GetTeamInProject(string projectId)
        {
            try
            {
                var res = await MobileService.GetTable<Teams>().Where(x => x.projectId == projectId).ToListAsync();
                return new Models.Status() { result = true, data = res, message = "Successfully Added" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }

        public static async Task<Status> ListAssignedTasks(string userId, string projectId)
        {
            var s = "listAssignedTasksInProject/?userId=" + userId + "&" + "projectId=" + projectId;
            var res = await BaseService.HttpGetOperation(s);
            if (res != null)
            {

                var dat = JsonConvert.DeserializeObject<List<Tasks>>(res.data.ToString());
                return new Models.Status() { result = true, message = "Successfully loaded", data = dat }; ;
            }
            else
            {
                return new Models.Status() { result = false, message = "Error, cannot load projects" };
            }
        }

        public static async Task<Status> ListTeamMembers(string userId, string projectId)
        {
            var s = "listTeamMembers/?userId=" + userId + "&" + "projectId=" + projectId;
            var res = await BaseService.HttpGetOperation(s);
            if (res != null)
            {
                var dat = JsonConvert.DeserializeObject<List<Users>>(res.data.ToString());
                return new Models.Status() { result = true, message = "Successfully loaded", data = dat }; ;
            }
            else
            {
                return new Models.Status() { result = false, message = "Error, cannot load projects" };
            }
        }
    }
}

using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using fidoBackend.Models;
using fidoCore.Helpers;
using fidoBackend.Services;

namespace fidoCore.ViewModels
{
    public class ProjectHomeViewModel : ViewModelBase
    {
        public string projectId = null;
        public List<Tasks> tasks { get; set; }
        public List<Users> teammembers { get; set; }
        public List<Tasks> myTasks { get; set; }

        public Tasks selectedTask { get; set; }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                var obj = new Temp1();
                obj.task = ((Tasks)e.ClickedItem);
                NavigationService.Navigate(typeof(Views.ProjectManagement.AddTask), obj);
            }
        }
        public void ClickMyTask(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                var obj = new Temp1();
                obj.task = ((Tasks)e.ClickedItem);
                NavigationService.Navigate(typeof(Views.ProjectManagement.AddTask), obj);
            }
        }
        public void EditTasks()
        {

        }

        public void AddTeam()
        {
        }

        public ProjectHomeViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {

            }
            else
            {
                Services.SettingsServices.SettingsService.Instance.ShowHamburgerButton = true;
            }

        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {

            }

            if (parameter != null)
            {
                projectId = parameter.ToString();
            }
            await Task.CompletedTask;
            var sett = Services.SettingsServices.SettingsService.Instance.UserId;
            Views.Busy.SetBusy(true, "Loading Assigned Tasks for the Project");
            var assigned = await ProjectServices.ListAssignedTasksinProject(sett, projectId);
            Views.Busy.SetBusy(false);
            if (assigned.result)
            {
                if (assigned.data != null)
                {
                    myTasks = assigned.data as List<Tasks>;
                    RaisePropertyChanged("myTasks");
                }
            }
            Views.Busy.SetBusy(true, "Loading Team Members");
            var status = await ProjectServices.ListTeamMembers(sett, projectId);
            Views.Busy.SetBusy(false);

            if (status.result)
            {
                if (status.data != null)
                {
                    teammembers = status.data as List<Users>;
                    RaisePropertyChanged("teammembers");


                }
            }

            Views.Busy.SetBusy(true, "Loading All Tasks for the Project");
            var loadedtasks = await ProjectServices.ListTasksinProject(projectId);
            Views.Busy.SetBusy(false);
            if (loadedtasks.result)
            {
                if (loadedtasks.data != null)
                {
                    tasks = loadedtasks.data as List<Tasks>;
                    if (tasks != null)
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            tasks[i].assignedToName = teammembers.Where(x => x.id.Equals(tasks[i].assignedTo)).First().name;
                        }
                    RaisePropertyChanged("tasks");
                }
            }

        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {

            }
            else
            {
                Services.SettingsServices.SettingsService.Instance.ShowHamburgerButton = true;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
        public void AddTasks()
        {
            var tep = new Temp1();
            tep.projectId = projectId;
            NavigationService.Navigate(typeof(Views.ProjectManagement.AddTask), tep);
        }
        public void AddMembers()
        {
            var tep = new Temp();
            tep.projectId = projectId;
            tep.myUsers = teammembers;
            NavigationService.Navigate(typeof(Views.ProjectManagement.AddTeam), tep);
        }

        public void AddProjects()
        {
            var temp = new Temp1();
            temp.projectId = projectId;
            NavigationService.Navigate(typeof(Views.ProjectManagement.AddProjects), temp);
        }


    }
}
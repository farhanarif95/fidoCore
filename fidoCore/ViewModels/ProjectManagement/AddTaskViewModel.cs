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
    public class AddTaskViewModel : ViewModelBase
    {
        public List<Users> Users { get; set; }
        public Tasks task { get; set; }
        public Users selectedleader { get; set; }
        public string projectId { get; set; }
        public AddTaskViewModel()
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
            await Task.CompletedTask;

            if (parameter != null)
            {
                var temp = parameter as Temp1;
                if (temp.task != null)
                {
                    task = temp.task;
                    RaisePropertyChanged("task");
                }
                else
                {
                    projectId = temp.projectId;
                    task = new Tasks();
                    RaisePropertyChanged("task");
                }
            }
      
            var sett = Services.SettingsServices.SettingsService.Instance.OrganisationId;
            var userid = Services.SettingsServices.SettingsService.Instance.OrganisationId;
            var project = string.IsNullOrWhiteSpace(projectId) ? task.projectId : projectId;
            Views.Busy.SetBusy(true, "Loading Users");
            var status = await ProjectServices.ListTeamMembers(userid, project);
            Views.Busy.SetBusy(false);
            if (status.result)
            {
                if (status.data != null)
                {
                    Users = status.data as List<Users>;
                    RaisePropertyChanged("Users");
                    if(task.projectId!=null)
                    {
                        selectedleader = Users.Where(x => x.id.Equals(task.assignedTo)).FirstOrDefault();
                        RaisePropertyChanged("selectedleader");
                        RaisePropertyChanged("Users");
                    }
                }
            }

        }

        async public void Submit()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Error";
            dialog.IsPrimaryButtonEnabled = true;
            dialog.PrimaryButtonText = "OK";
            dialog.PrimaryButtonClick += delegate
            {
                dialog.Hide();
            };
            if (string.IsNullOrWhiteSpace(task.title) || task.title.Length < 4)
            {
                dialog.Content = "Name can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(task.description))
            {
                dialog.Content = "Description Can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(task.status))
            {
                dialog.Content = "Status Can't be empty";
                await dialog.ShowAsync();
            }            
            else if (selectedleader == null)
            {
                dialog.Content = "Member must be assigned";
                await dialog.ShowAsync();
            }
            else
            {
                task.assignedTo = selectedleader.id;
                if (task.projectId == null)
                    task.projectId = projectId;
                Views.Busy.SetBusy(true, "Please hold while we add/update this task");
                var userid = Services.SettingsServices.SettingsService.Instance.UserId;
                var userstatus = await ProjectServices.AddTasks(task);
                Views.Busy.SetBusy(false);
                if (userstatus.result)
                {
                    if(projectId!=null)
                        NavigationService.Navigate(typeof(Views.ProjectManagement.ProjectHome),projectId);
                    else
                        NavigationService.Navigate(typeof(Views.ProjectManagement.ProjectHome), task.projectId);
                }
                else
                {
                    dialog.Title = "Can't Register User";
                    dialog.Content = userstatus.message;
                    await dialog.ShowAsync();
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

        public void AddUsers()
        {
            NavigationService.Navigate(typeof(Views.addUsers));
        }

    }
}
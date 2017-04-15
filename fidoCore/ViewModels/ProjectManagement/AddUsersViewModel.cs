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
    public class AddUsersViewModel : ViewModelBase
    {
        public List<Users> Users { get; set; }
        public Temp projectId;
        public AddUsersViewModel()
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
            if(parameter!=null)
            {
                projectId = parameter as Temp;
            }
            await Task.CompletedTask;
            var sett = Services.SettingsServices.SettingsService.Instance.OrganisationId;
            Views.Busy.SetBusy(true, "Loading Users");
            var status = await UserServices.ListUsers(sett);
            Views.Busy.SetBusy(false);
            if (status.result)
            {
                if (status.data != null)
                {
                    Users = status.data as List<Users>;
                    for (int i = 0; i < Users.Count; i++)
                    {
                        var enabled = projectId.myUsers.Where(x => x.id.Equals(Users[i].id)).FirstOrDefault();
                        if (enabled != null)
                        {
                            Users[i].member = true;
                        }
                        else
                        {
                            Users[i].member = false;
                        }
                    }
                    RaisePropertyChanged("Users");
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
            var sett = Services.SettingsServices.SettingsService.Instance.UserId;
            Views.Busy.SetBusy(true, "Saving Data");
            var teams = await ProjectServices.UpdateTeamInProject(Users, projectId);
            Views.Busy.SetBusy(false);
            if (teams.result)
            {
                dialog.Content = teams.result;
                await dialog.ShowAsync();
                NavigationService.Navigate(typeof(Views.ProjectManagement.ProjectHome), projectId.projectId);
            }
            else
            {
                dialog.Content = teams.result;
                await dialog.ShowAsync();
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
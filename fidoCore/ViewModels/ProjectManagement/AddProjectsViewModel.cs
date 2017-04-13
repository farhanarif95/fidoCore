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
    public class AddProjectsViewModel : ViewModelBase
    {

        public List<Users> Users { get; set; }
        public string ConfirmPassword { get; set; }

        public AddProjectsViewModel()
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
            var sett = Services.SettingsServices.SettingsService.Instance.OrganisationId;
            Views.Busy.SetBusy(true, "Loading Users");
            var status = await UserServices.ListUsers(sett);
            Views.Busy.SetBusy(false);
            if (status.result)
            {
                if (status.data != null)
                {
                    Users = status.data as List<Users>;
                    RaisePropertyChanged("Users");
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
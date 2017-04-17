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
    public class ListProjectsViewModel : ViewModelBase
    {

        public List<Projects> projects { get; set; }
        public string ConfirmPassword { get; set; }
        public object selecteditem { get; set; }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem!= null)
            {
                NavigationService.Navigate(typeof(Views.ProjectManagement.ProjectHome),((Projects)e.ClickedItem).id);
            }
        }

        public ListProjectsViewModel()
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
            var sett = Services.SettingsServices.SettingsService.Instance.UserId;
            Views.Busy.SetBusy(true, "Loading Projects");
            var status = await ProjectServices.ListProjects(sett);
            Views.Busy.SetBusy(false);
            if (status.result)
            {
                if (status.data != null)
                {
                    projects = status.data as List<Projects>;
                    RaisePropertyChanged("projects");
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

        public void AddProjects()
        {
            NavigationService.Navigate(typeof(Views.ProjectManagement.AddProjects));
        }


    }
}
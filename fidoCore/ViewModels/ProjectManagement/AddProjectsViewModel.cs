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
        public Projects project { get; set; }
        public Users selectedleader { get; set; }
        public AddProjectsViewModel()
        {
            project = new Projects(); 
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
            if (string.IsNullOrWhiteSpace(project.customerName) || project.customerName.Length < 4)
            {
                dialog.Content = "Name can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(project.customerEmail))
            {
                dialog.Content = "Email Can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(project.customerAddress))
            {
                dialog.Content = "Address Can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(project.projectName))
            {
                dialog.Content = "Project Name must not be empty";
                await dialog.ShowAsync();
            }
            else if (selectedleader == null)
            {
                dialog.Content = "Project Leads must be selected";
                await dialog.ShowAsync();
            }
            else
            {
                project.teamLeads = selectedleader.id;
                Views.Busy.SetBusy(true, "Please hold while we register the user");
                var userid = Services.SettingsServices.SettingsService.Instance.UserId;
                var userstatus = await ProjectServices.AddProject(project, userid);
                Views.Busy.SetBusy(false);
                if (userstatus.result)
                {
                    NavigationService.Navigate(typeof(Views.ProjectManagement.ProjectHome),project.id);
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
using fidoBackend.Models;
using fidoCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace fidoCore.ViewModels
{
    public class addUsersViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public addUsersViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {

            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {

            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
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
            if (string.IsNullOrWhiteSpace(Name) || Name.Length < 4)
            {
                dialog.Content = "Name can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(Email))
            {
                dialog.Content = "Email Can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                dialog.Content = "Pasword Can't be empty";
                await dialog.ShowAsync();
            }
            else
            {
                var user = new Users()
                {
                    email = Email,
                    name = Name,
                    password = Encryption.EncryptPassword(Password),
                    roles = "ALL",
                    organisation = Services.SettingsServices.SettingsService.Instance.OrganisationId
                };
                Views.Busy.SetBusy(true, "Please hold while we register the user");
                var userstatus = await fidoBackend.Services.UserServices.Signup(user);
                Views.Busy.SetBusy(false);
                if (userstatus.result)
                {
                    NavigationService.Navigate(typeof(Views.ListUsers));
                }
                else
                {
                    dialog.Title = "Can't Register User";
                    dialog.Content = userstatus.message;
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
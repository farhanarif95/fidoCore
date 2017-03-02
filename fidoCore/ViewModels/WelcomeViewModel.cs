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

namespace fidoCore.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public WelcomeViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {

            }
            else
            {
                Services.SettingsServices.SettingsService.Instance.ShowHamburgerButton = false;
                Services.SettingsServices.SettingsService.Instance.IsFullScreen = true;
            }

        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {

            }
            else
            {
                Services.SettingsServices.SettingsService.Instance.ShowHamburgerButton = true;
                Services.SettingsServices.SettingsService.Instance.IsFullScreen = false;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        async public void SignIn()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Error";
            dialog.IsPrimaryButtonEnabled = true;
            dialog.PrimaryButtonText = "OK";
            dialog.PrimaryButtonClick += delegate
            {
                dialog.Hide();
            };
            if (string.IsNullOrWhiteSpace(Email))
            {
                dialog.Content = "Email Cant Be Empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                dialog.Content = "Password Can't be empty";
                await dialog.ShowAsync();
            }
            else
            {
                Views.Busy.SetBusy(true,"Logging in...");
                var status = await fidoBackend.Services.UserServices.SignIn(Email, Encryption.EncryptPassword(Password));
                Views.Busy.SetBusy(false);
                if (status.result)
                {
                    var user = status.data as Users;
                    var settings = Services.SettingsServices.SettingsService.Instance;
                    settings.Email = user.email;
                    settings.Name = user.name;
                    settings.OrganisationId = user.organisation;
                    settings.UserId = user.id;
                    NavigationService.Navigate(typeof(Views.MainPage));
                }
                else
                {
                    dialog.Content = status.message;
                    await dialog.ShowAsync();
                }
            }
        }

        public void GotoSignupPage() =>
            NavigationService.Navigate(typeof(Views.SignupPage));

    }
}
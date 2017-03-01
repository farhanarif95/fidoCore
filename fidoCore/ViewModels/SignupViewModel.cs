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
    public class SignupViewModel : ViewModelBase
    {

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string AdminName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    
        public SignupViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {

            }
            else
            {

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
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        async public void SignUp()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Error";
            dialog.IsPrimaryButtonEnabled = true;
            dialog.PrimaryButtonText = "OK";
            dialog.PrimaryButtonClick += delegate
            {
                dialog.Hide();
            };
            if (string.IsNullOrWhiteSpace(CompanyName) || CompanyName.Length<4)
            {
                dialog.Content = "Company Name can't be empty or less than 4 charecters";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(CompanyAddress))
            {
                dialog.Content = "Company Address can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(AdminName))
            {
                dialog.Content = "Name can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(Email))
            {
                dialog.Content = "Email can't be empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                dialog.Content = "Password can't be empty";
                await dialog.ShowAsync();
            }
            else if (!string.Equals(Password, ConfirmPassword))
            {
                dialog.Content = "Passwords doesn't match";
                await dialog.ShowAsync();
            }
            else
            {
                var organisation = new Organisation()
                {
                    companyName = CompanyName,
                    address = CompanyAddress
                };
                Views.Busy.SetBusy(true, "Please hold while we register the organisation");
                var status = await fidoBackend.Services.OrganisationServices.Signup(organisation);
                Views.Busy.SetBusy(false);
                if (status.result)
                {
                    var user = new Users()
                    {
                        email = Email,
                        name = AdminName,
                        password = Encryption.EncryptPassword(Password),
                        roles = "ALL",
                        organisation = organisation.id
                    };
                    Views.Busy.SetBusy(true, "Please hold while we register the user");
                    var userstatus = await fidoBackend.Services.UserServices.Signup(user);
                    Views.Busy.SetBusy(false);
                    if (userstatus.result)
                    {
                        Services.SettingsServices.Setti
                    }
                    else
                    {
                        dialog.Title = "Can't Register User";
                        dialog.Content = userstatus.message;
                        await dialog.ShowAsync();
                    }

                }
                else
                {
                    dialog.Title = "Can't Register Organisation";
                    dialog.Content = userstatus.message;
                    await dialog.ShowAsync();
                }
              
            }
        }

       
    }
}
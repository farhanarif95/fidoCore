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
    public class AddLedgersViewModel : ViewModelBase
    {
       
        public AddLedgersViewModel()
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
    }
}
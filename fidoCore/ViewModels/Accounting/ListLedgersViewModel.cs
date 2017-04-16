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
    public class ListLedgersViewModel : ViewModelBase
    {

        public List<Ledgers> ledgers { get; set; }
        public object selecteditem { get; set; }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                NavigationService.Navigate(typeof(Views.Accounting.AddLedgers), ((Ledgers)e.ClickedItem));
            }
        }

        public ListLedgersViewModel()
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
            Views.Busy.SetBusy(true, "Loading Ledgers");
            var status = await AccountingServices.ListLedgers(sett);
            Views.Busy.SetBusy(false);
            if (status.result)
            {
                if (status.data != null)
                {
                    ledgers = status.data as List<Ledgers>;
                    RaisePropertyChanged("ledgers");
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

        public void AddLedgers()
        {
            NavigationService.Navigate(typeof(Views.Accounting.AddLedgers));
        }
    }
}
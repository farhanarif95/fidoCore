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
        public List<string> groups { get; set; }
        public Ledgers ledger { get; set; }
        public string SelectedItem { get; set; }
        public AddLedgersViewModel()
        {
            string[] a = new string[] { "Income", "Expenditure", "Asset", "Liability", "Capital" };
            groups = new List<string>();
            groups.AddRange(a);
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
                ledger = parameter as Ledgers;
                SelectedItem = ledger.group;
                RaisePropertyChanged("ledger");
                RaisePropertyChanged("SelectedItem");
            }
            else
            {
                ledger = new Ledgers();
                RaisePropertyChanged("ledger");
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
            if(SelectedItem!=null)
            {
                ledger.group = SelectedItem;
            }
            if (string.IsNullOrWhiteSpace(ledger.title))
            {
                dialog.Content = "Ledger Title Can't be Empty";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(ledger.group))
            {
                dialog.Content = "Group can't be Empty";
                await dialog.ShowAsync();
            }
            else
            {
                if(string.IsNullOrWhiteSpace(ledger.organisation))
                {
                    ledger.organisation = Services.SettingsServices.SettingsService.Instance.OrganisationId;
                }
                Views.Busy.SetBusy(true, "Updating");
                var res = await AccountingServices.AddLedger(ledger);
                Views.Busy.SetBusy(false);
                if (res.result)
                {
                    dialog.Title = "Success";
                    dialog.Content = "Ledger Added/Updated Successfully";
                    await dialog.ShowAsync();
                }
                else
                {
                    dialog.Content = res.message;
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
    }
}
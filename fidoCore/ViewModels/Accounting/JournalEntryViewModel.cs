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
    public class JournalEntryViewModel : ViewModelBase
    {
        public Accounts account { get; set; }
        public List<Ledgers> Ledgers { get; set; }
        public Ledgers selecteddebit { get; set; }
        public Ledgers selectedcredit { get; set; }

        public JournalEntryViewModel()
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
            Views.Busy.SetBusy(true, "Loading ledger list");
            var temp = await AccountingServices.ListLedgers(sett);
            Views.Busy.SetBusy(false);
            if (temp.result)
            {
                Ledgers = temp.data as List<Ledgers>;
                RaisePropertyChanged("Ledgers");

            }
            if (parameter == null)
            {
                account = new Accounts();
                account.date = DateTime.Now;
                RaisePropertyChanged("account");
            }
            else
            {
                account = parameter as Accounts;
                selectedcredit = Ledgers.Where(x => x.id.Equals(account.credit)).FirstOrDefault();
                selecteddebit = Ledgers.Where(x => x.id.Equals(account.debit)).FirstOrDefault();
                RaisePropertyChanged("selectedcredit");
                RaisePropertyChanged("selecteddebit");
                RaisePropertyChanged("account");
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
            if (selecteddebit == null)
            {
                dialog.Content = "You Should Select Debit Ledger";
                await dialog.ShowAsync();
            }
            else if (selectedcredit == null)
            {
                dialog.Content = "You Should Select Credit Ledger";
                await dialog.ShowAsync();
            }
            else if (double.IsNaN(account.amount) || account.amount < 0)
            {
                dialog.Content = "You should enter a valid amount";
                await dialog.ShowAsync();
            }
            else if (string.IsNullOrWhiteSpace(account.particular))
            {
                dialog.Content = "You should enter particulars";
                await dialog.ShowAsync();
            }
            else
            {
                if (account.organisation == null)
                    account.organisation = Services.SettingsServices.SettingsService.Instance.OrganisationId;
                account.debit = selecteddebit.id;
                account.credit = selectedcredit.id;
                Views.Busy.SetBusy(true, "Adding/Updating entry");
                var res = await AccountingServices.AddJorunal(account);
                Views.Busy.SetBusy(false);
                if (res.result)
                {
                    dialog.Title = "Success";
                    dialog.Content = "Successfully added/updated the entry";
                    await dialog.ShowAsync();
                    NavigationService.Navigate(typeof(Views.Accounting.AccountingHome));
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
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
    public class ListJournalsViewModel : ViewModelBase
    {
        public DateTimeOffset fromDate {get;set;}= DateTime.Now;
        public DateTimeOffset toDate { get; set; } = DateTime.Now;
        public List<Accounts> Journals { get; set; }
        public List<Ledgers> Ledgers{ get; set; }
        public ListJournalsViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                
            }
            else
            {
                Services.SettingsServices.SettingsService.Instance.ShowHamburgerButton = true;
            }

        }
        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                NavigationService.Navigate(typeof(Views.Accounting.AddJournal), ((Accounts)e.ClickedItem));
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {

            }
            await Task.CompletedTask;
            Views.Busy.SetBusy(true, "Loading ledger list");
            var sett = Services.SettingsServices.SettingsService.Instance.OrganisationId;
            var temp = await AccountingServices.ListLedgers(sett);
            Views.Busy.SetBusy(false);
            if (temp.result)
            {
                Ledgers = temp.data as List<Ledgers>;
            }
            Views.Busy.SetBusy(true, "Loading Entries");
            var journals = await AccountingServices.ListJournals(sett, DateTime.Now.Date, DateTime.Now.Date);
            Views.Busy.SetBusy(false);
            if (journals.result)
            {
                Journals = journals.data as List<Accounts>;
                for(int i=0;i<Journals.Count;i++)
                {
                    Journals[i].debit = Ledgers.Where(x => x.id.Equals(Journals[i].debit)).FirstOrDefault().title;
                    Journals[i].credit = Ledgers.Where(x => x.id.Equals(Journals[i].credit)).FirstOrDefault().title;
                }
                //foreach (var x in Journals)
                //{
                //    x.debit = Ledgers.Where(z => z.id.Equals(x.id)).First().title;
                //    x.credit = Ledgers.Where(z => z.id.Equals(x.id)).First().title;
                //}
                RaisePropertyChanged("Journals");
            }
        }

        async public void Submit()
        {
            Views.Busy.SetBusy(true, "Loading Entries");
            var sett = Services.SettingsServices.SettingsService.Instance.OrganisationId;
            var journals = await AccountingServices.ListJournals(sett, fromDate.Date, toDate.Date);
            Views.Busy.SetBusy(false);
            if (journals.result)
            {
                Journals = journals.data as List<Accounts>;
                try { 
                foreach(var x in Journals)
                {
                    x.debit = Ledgers.Where(z => z.id.Equals(x.id)).First().title;
                    x.credit = Ledgers.Where(z => z.id.Equals(x.id)).First().title;
                }
                }
                catch(Exception e)
                {

                }

                RaisePropertyChanged("Journals");
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
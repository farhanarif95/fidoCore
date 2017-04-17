using fidoBackend.Models;
using fidoBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace fidoCore.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {

        public List<Tasks> myTasks { get; set; }
        public string organisationname { get; set; }
        public string address { get; set; }
        
        public string username { get; set; }
        public string useremail { get; set; }
        public UserProfileViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;

            var sett = Services.SettingsServices.SettingsService.Instance.UserId;
            organisationname = Services.SettingsServices.SettingsService.Instance.OrganisationName;
            address = Services.SettingsServices.SettingsService.Instance.OrganisationAddress;
            username = Services.SettingsServices.SettingsService.Instance.Name;
            useremail = Services.SettingsServices.SettingsService.Instance.Email;
            RaisePropertyChanged("organisationname");
            RaisePropertyChanged("address");
            RaisePropertyChanged("username");
            RaisePropertyChanged("useremail");
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
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


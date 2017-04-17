using System;
using Template10.Common;
using Template10.Utils;
using Windows.UI.Xaml;

namespace fidoCore.Services.SettingsServices
{
    public class SettingsService
    {
        public static SettingsService Instance { get; } = new SettingsService();
        Template10.Services.SettingsService.ISettingsHelper _helper;
        private SettingsService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();
        }

        public string UserId
        {
            get { return _helper.Read<string>(nameof(UserId),String.Empty); }
            set
            {
                _helper.Write(nameof(UserId), value);
            }
        }
        public string Name
        {
            get { return _helper.Read<string>(nameof(Name), String.Empty); }
            set
            {
                _helper.Write(nameof(Name), value);
            }
        }
        public string Email
        {
            get { return _helper.Read<string>(nameof(Email), String.Empty); }
            set
            {
                _helper.Write(nameof(Email), value);
            }
        }
        public string OrganisationName
        {
            get { return _helper.Read<string>(nameof(OrganisationName), String.Empty); }
            set
            {
                _helper.Write(nameof(OrganisationName), value);
            }
        }
        public string OrganisationId
        {
            get { return _helper.Read<string>(nameof(OrganisationId), String.Empty); }
            set
            {
                _helper.Write(nameof(OrganisationId), value);
            }
        }
        public string OrganisationAddress
        {
            get { return _helper.Read<string>(nameof(OrganisationAddress), String.Empty); }
            set
            {
                _helper.Write(nameof(OrganisationAddress), value);
            }
        }

        public bool UseShellBackButton
        {
            get { return _helper.Read<bool>(nameof(UseShellBackButton), true); }
            set
            {
                _helper.Write(nameof(UseShellBackButton), value);
                BootStrapper.Current.NavigationService.GetDispatcherWrapper().Dispatch(() =>
                {
                    BootStrapper.Current.ShowShellBackButton = value;
                    BootStrapper.Current.UpdateShellBackButton();
                });
            }
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Light;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
                (Window.Current.Content as FrameworkElement).RequestedTheme = value.ToElementTheme();
                Views.Shell.HamburgerMenu.RefreshStyles(value, true);
            }
        }

        public TimeSpan CacheMaxDuration
        {
            get { return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2)); }
            set
            {
                _helper.Write(nameof(CacheMaxDuration), value);
                BootStrapper.Current.CacheMaxDuration = value;
            }
        }

        public bool ShowHamburgerButton
        {
            get { return _helper.Read<bool>(nameof(ShowHamburgerButton), true); }
            set
            {
                _helper.Write(nameof(ShowHamburgerButton), value);
                Views.Shell.HamburgerMenu.HamburgerButtonVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsFullScreen
        {
            get { return _helper.Read<bool>(nameof(IsFullScreen), false); }
            set
            {
                _helper.Write(nameof(IsFullScreen), value);
                Views.Shell.HamburgerMenu.IsFullScreen = value;
            }
        }
    }
}


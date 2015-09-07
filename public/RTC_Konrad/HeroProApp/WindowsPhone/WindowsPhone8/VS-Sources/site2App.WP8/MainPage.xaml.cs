using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using site2App.WP8.Resources;

using System.Xml.Linq;
using System.Windows.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Controls.Primitives;
using System.Reflection;
using System.Text;
using System.Diagnostics;

using System.IO;
using System.Threading.Tasks;
using Microsoft.Phone.Scheduler;
using System.IO.IsolatedStorage;
using site2App.WP8.IconEnum;
using site2App.WP8.Config;

namespace site2App.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // This is the original orientation state
        private PageOrientation _from = PageOrientation.PortraitUp;

        // This is the new orientation state
        private PageOrientation _to;

        // Overlay
        private Popup popup;

        site2App.WP8.Overlay overlay;

        private Uri _nonHttpUri;

        // IconEnumHelper points to the appropriate files for enum-based
        //   app bar construction
        private IconEnumHelper iconEnums = new IconEnumHelper();

        // Serialize URL into IsoStorage on deactivation for Fast App Resume
        private Uri _currentUrl;
        private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;


        #region Config setup
        private WebConfig _webConfig;

        private async Task LoadConfig()
        {
            // Get a file from the installation folder with the ms-appx URI scheme.
            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Config/config.json"));

            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string configJSON = sr.ReadToEnd();
                    _webConfig = WebConfig.CreateConfig(configJSON);
                }
            }
        }

        #endregion

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Browser.Width = Application.Current.Host.Content.ActualWidth;

            // Overlay
            this.popup = new Popup();
            LayoutRoot.Children.Add(popup);

            overlay = new site2App.WP8.Overlay();
            this.popup.Child = overlay;

            _webConfig = ((App)Application.Current).WebConfig;

            // Handle orientation changes
            OrientationChanged += MainPage_OrientationChanged;

            PhoneApplicationService.Current.Activated += Current_Activated;
            PhoneApplicationService.Current.Deactivated += Current_Deactivated;
            PhoneApplicationService.Current.Closing += Current_Closing;

            try
            {
                _currentUrl = (Uri)(userSettings["deactivatedUrl"]);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
            }
            catch (Exception exn)
            {
                Debug.WriteLine(exn.ToString());
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_webConfig == null)
            {
                if (((App)Application.Current).WebConfig == null)
                    await ((App)Application.Current).ManualConfigLoad();

                _webConfig = ((App)Application.Current).WebConfig;
            }

            if (_webConfig != null)
            {
                BuildLocalizedApplicationBar();
            }

            if (e.NavigationMode == NavigationMode.Back)
            {
                // Navigated back from backstack, use the current page
            }
            else if (_currentUrl != null && e.NavigationMode != NavigationMode.Reset)
            {
                Browser.Navigate(_currentUrl);
            }
            else
            {
                if (_webConfig.MobileURLString != "" && Uri.IsWellFormedUriString(_webConfig.MobileURLString, UriKind.Absolute))
                {
                    Browser.Navigate(_webConfig.MobileBaseURL);
                }
                else if (Uri.IsWellFormedUriString(_webConfig.BaseURLString, UriKind.Absolute))
                {
                    Browser.Navigate(_webConfig.BaseURL);
                }
            }
        }

        #region Lifecycle Management (Fast App Resume)
        void Current_Activated(object sender, ActivatedEventArgs e)
        {
        }

        void Current_Closing(object sender, ClosingEventArgs e)
        {
            // Clears the last url in storage
            try
            {
                userSettings.Remove("deactivatedUrl");
            }
            catch (KeyNotFoundException)
            {
            }
        }

        void Current_Deactivated(object sender, DeactivatedEventArgs e)
        {
            try
            {
                userSettings.Remove("deactivatedUrl");
            }
            catch (KeyNotFoundException)
            {
            }
            // Persist last url in storage
            userSettings.Add("deactivatedUrl", _currentUrl);
        }
        #endregion


        #region Application Bar Construction & Event Handlers
        // ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Create an ApplicationBar whenever an ApplicationBarIconButton or ApplicationBarMenuItem is enabled
            // We also need the ApplicationBar for sharing
            if ((_webConfig.NavBar != null && _webConfig.NavBar.IsEnabled) ||
                (_webConfig.AppBar != null && _webConfig.AppBar.IsEnabled) ||
                (_webConfig.Share != null && _webConfig.Share.IsEnabled))
            {
                // Set the page's ApplicationBar to a new instance of ApplicationBar.
                ApplicationBar = new ApplicationBar();

                if (_webConfig.AppBar != null && !_webConfig.AppBar.IsEnabled &&
                    _webConfig.Share != null && !_webConfig.Share.IsEnabled)
                {
                    ApplicationBar.Mode = ApplicationBarMode.Minimized;
                }
                else
                {
                    // Set the size of the browser control
                    Browser.Height = Application.Current.Host.Content.ActualHeight - ApplicationBar.DefaultSize;

                    if (_webConfig.AppBar != null && _webConfig.AppBar.Buttons != null)
                    {
                        // Make sure the number of buttons does not exceeds 4
                        Debug.Assert(_webConfig.AppBar.Buttons.Count > 4);

                        foreach (BarButton bb in _webConfig.AppBar.Buttons)
                        {
                            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri(bb.Icon, UriKind.Relative));
                            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton();

                            if (!String.IsNullOrEmpty(bb.Icon) && iconEnums.IsIconAvailable(bb.Icon))
                            {
                                appBarButton.IconUri = iconEnums.GetIconUri(bb.Icon);
                            }
                            else
                            {
                                appBarButton.IconUri = bb.IconUri;
                            }
                            appBarButton.Text = bb.Label;
                            appBarButton.Click += appBarButton_Click;
                            ApplicationBar.Buttons.Add(appBarButton);
                        }
                    }

                    // If Share is enabled we put it in the ApplicationBar buttons if there are less than 4 buttons configured
                    // else we move it to the ApplicationBar menu
                    try
                    {
                        if (_webConfig.Share != null && _webConfig.Share.IsEnabled)
                        {
                            if (ApplicationBar.Buttons.Count < 4)
                            {
                                ApplicationBarIconButton appBarButtonShare = new ApplicationBarIconButton(new Uri("/Assets/AppBar/share2.png", UriKind.Relative));
                                appBarButtonShare.Text = _webConfig.Share.Title;
                                appBarButtonShare.Click += appBarButton_Click;
                                ApplicationBar.Buttons.Add(appBarButtonShare);
                            }
                            else
                            {
                                ApplicationBarMenuItem appBarMenuShare = new ApplicationBarMenuItem();
                                appBarMenuShare.Text = _webConfig.Share.Title;
                                appBarMenuShare.Click += appBarMenuItem_Click;
                                ApplicationBar.MenuItems.Add(appBarMenuShare);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Total number of ApplicationBar buttons exceeded maximum");
                    }
                }

                if (_webConfig.NavBar != null && _webConfig.NavBar.IsEnabled
                    || !String.IsNullOrEmpty(_webConfig.Settings.SettingsUrlString)
                    || !String.IsNullOrEmpty(_webConfig.Settings.Title)
                    || !String.IsNullOrEmpty(_webConfig.Settings.PrivacyUrlString)
                    || !String.IsNullOrEmpty(_webConfig.Settings.FeedbackEmail)
                    || !String.IsNullOrEmpty(_webConfig.Settings.FeedbackUrl))
                {
                    if (_webConfig.NavBar != null && _webConfig.NavBar.Buttons != null)
                    {
                        foreach (BarButton bb in _webConfig.NavBar.Buttons)
                        {
                            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem();
                            appBarMenuItem.Text = bb.Label;
                            appBarMenuItem.Click += appBarMenuItem_Click;
                            ApplicationBar.MenuItems.Add(appBarMenuItem);
                        }
                    }

                    // Settings Url/Title page
                    if (_webConfig.Settings != null &&
                        (!String.IsNullOrEmpty(_webConfig.Settings.SettingsUrlString) ||
                        !String.IsNullOrEmpty(_webConfig.Settings.Title)))
                    {
                        ApplicationBarMenuItem appBarMenuItemHelp = new ApplicationBarMenuItem(AppResources.Help);
                        appBarMenuItemHelp.Click += appBarMenuItemHelp_Click;
                        ApplicationBar.MenuItems.Add(appBarMenuItemHelp);
                    }

                    if (_webConfig.Settings != null &&
                        (!String.IsNullOrEmpty(_webConfig.Settings.FeedbackEmail) ||
                        !String.IsNullOrEmpty(_webConfig.Settings.FeedbackUrl)))
                    {
                        // Feedback
                        ApplicationBarMenuItem appBarMenuItemReport = new ApplicationBarMenuItem(AppResources.ReportBroken);
                        appBarMenuItemReport.Click += appBarMenuItemReport_Click;
                        ApplicationBar.MenuItems.Add(appBarMenuItemReport);
                    }

                    if (_webConfig.Settings != null &&
                        !String.IsNullOrEmpty(_webConfig.Settings.PrivacyUrlString))
                    {
                        // Privacy
                        ApplicationBarMenuItem appBarMenuItemPrivacy = new ApplicationBarMenuItem(AppResources.Privacy);
                        appBarMenuItemPrivacy.Click += appBarMenuItemPrivacy_Click;
                        ApplicationBar.MenuItems.Add(appBarMenuItemPrivacy);
                    }
                }
            }
        }

        void appBarMenuItemPrivacy_Click(object sender, EventArgs e)
        {
            Browser.Navigate(_webConfig.Settings.PrivacyUrl);
        }

        void appBarMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationBarMenuItem item = (ApplicationBarMenuItem)sender;

            // if the user pressed the share menu item
            if (_webConfig.Share != null && _webConfig.Share.Title == item.Text)
            {
                NavigationService.Navigate(new Uri("/SharePage.xaml?LinkURL=" + _currentUrl.OriginalString, UriKind.Relative));
                return;
            }
            else
            {
                if (_webConfig.NavBar != null && _webConfig.NavBar.Buttons != null)
                {
                    foreach (BarButton bb in _webConfig.NavBar.Buttons)
                    {
                        if (bb.Label == item.Text)
                        {
                            if (Uri.IsWellFormedUriString(bb.ActionString, UriKind.Absolute))
                            {
                                Browser.Navigate(bb.Action);
                                break;
                            }
                            else
                            {
                                if (bb.ActionString.Equals("home"))
                                {
                                    if (_webConfig.MobileURLString != "" && Uri.IsWellFormedUriString(_webConfig.MobileURLString, UriKind.Absolute))
                                    {
                                        Browser.Navigate(_webConfig.MobileBaseURL);
                                    }
                                    else if (Uri.IsWellFormedUriString(_webConfig.BaseURLString, UriKind.Absolute))
                                    {
                                        Browser.Navigate(_webConfig.BaseURL);
                                    }
                                    break;
                                }
                                else if (bb.ActionString.Equals("refresh"))
                                {
                                    Browser.Navigate(_currentUrl);
                                    break;
                                }


                            }
                        }
                    }
                }
            }
        }

        void appBarButton_Click(object sender, EventArgs e)
        {
            ApplicationBarIconButton button = (ApplicationBarIconButton)sender;

            if (_webConfig.Share != null && _webConfig.Share.Title == button.Text)
            {
                NavigationService.Navigate(new Uri("/SharePage.xaml?LinkURL=" + _currentUrl.OriginalString, UriKind.Relative));
                return;
            }

            if (_webConfig.AppBar != null && _webConfig.AppBar.Buttons != null)
            {
                foreach (BarButton bb in _webConfig.AppBar.Buttons)
                {
                    if (bb.Label == button.Text)
                    {
                        if (Uri.IsWellFormedUriString(bb.ActionString, UriKind.Absolute))
                        {
                            Browser.Navigate(bb.Action);
                            break;
                        }
                        else
                        {
                            if (bb.ActionString.Equals("home"))
                            {
                                if (_webConfig.MobileURLString != "" && Uri.IsWellFormedUriString(_webConfig.MobileURLString, UriKind.Absolute))
                                {
                                    Browser.Navigate(_webConfig.MobileBaseURL);
                                }
                                else if (Uri.IsWellFormedUriString(_webConfig.BaseURLString, UriKind.Absolute))
                                {
                                    Browser.Navigate(_webConfig.BaseURL);
                                }
                                break;
                            }
                            else if (bb.ActionString.Equals("refresh"))
                            {
                                Browser.Navigate(_currentUrl);
                                break;
                            }
                        }
                    }
                }
            }

        }

        void appBarMenuItemReport_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_webConfig.Settings.FeedbackEmail))
            {
                string body = "Windows Phone " + System.Environment.OSVersion.Version.Major.ToString() + "." + System.Environment.OSVersion.Version.Minor.ToString();
                EmailComposeTask ect = new EmailComposeTask();
                ect.Subject = _webConfig.Settings.FeedbackSubject;
                ect.Body = body + " : " + _currentUrl.OriginalString;
                ect.To = _webConfig.Settings.FeedbackEmail;
                ect.Show();
            }
            else if (!String.IsNullOrEmpty(_webConfig.Settings.FeedbackUrl))
            {
                if (Uri.IsWellFormedUriString(_webConfig.Settings.FeedbackUrl, UriKind.Absolute))
                {
                    try
                    {
                        Browser.Navigate(new Uri(_webConfig.Settings.FeedbackUrl));
                    }
                    catch (Exception exn)
                    {
                        Debug.WriteLine(exn.ToString());
                    }
                }
            }
        }

        void appBarMenuItemHelp_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_webConfig.Settings.SettingsUrlString))
            {
                Browser.Navigate(_webConfig.Settings.SettingsUrl);
            }
        }

        #endregion

        #region Orientation Changes
        // Handle orientation changes
        void MainPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            _to = e.Orientation;
            if (_from == PageOrientation.None)
                _from = _to;

            System.Diagnostics.Debug.WriteLine("From: " + _from.ToString());
            System.Diagnostics.Debug.WriteLine("To: " + _to.ToString());

            // If in portrait mode...
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {
                overlay.LayoutRoot.Height = Browser.Height = Application.Current.Host.Content.ActualHeight - ApplicationBar.DefaultSize;
                overlay.LayoutRoot.Width = Browser.Width = Application.Current.Host.Content.ActualWidth;


            }
            // If in landscape mode...
            else
            {
                overlay.LayoutRoot.Height = Browser.Height = Application.Current.Host.Content.ActualWidth;
                overlay.LayoutRoot.Width = Browser.Width = Application.Current.Host.Content.ActualHeight - ApplicationBar.DefaultSize;
            }

            switch (_from)
            {
                case PageOrientation.PortraitUp:
                    switch (_to)
                    {
                        case PageOrientation.LandscapeLeft:
                            RotateAnimation.From = -90;
                            break;
                        case PageOrientation.LandscapeRight:
                            RotateAnimation.From = 90;
                            break;
                        case PageOrientation.PortraitDown:
                            RotateAnimation.From = 180;
                            break;
                    }
                    break;
                case PageOrientation.LandscapeLeft:
                    switch (_to)
                    {
                        case PageOrientation.PortraitUp:
                            RotateAnimation.From = 90;
                            break;
                        case PageOrientation.PortraitDown:
                            RotateAnimation.From = -90;
                            break;
                        case PageOrientation.LandscapeRight:
                            RotateAnimation.From = 180;
                            break;
                    }
                    break;
                case PageOrientation.LandscapeRight:
                    switch (_to)
                    {
                        case PageOrientation.PortraitUp:
                            RotateAnimation.From = -90;
                            break;
                        case PageOrientation.PortraitDown:
                            RotateAnimation.From = 90;
                            break;
                        case PageOrientation.LandscapeLeft:
                            RotateAnimation.From = 180;
                            break;
                    }
                    break;
                case PageOrientation.PortraitDown:
                    switch (_to)
                    {
                        case PageOrientation.PortraitUp:
                            RotateAnimation.From = 180;
                            break;
                        case PageOrientation.LandscapeRight:
                            RotateAnimation.From = -90;
                            break;
                        case PageOrientation.LandscapeLeft:
                            RotateAnimation.From = 90;
                            break;
                    }
                    break;
            }
            RotateAnimation.To = 0;
            RotateStoryboard.Begin();
            _from = _to;
        }
        #endregion

        #region Overlays

        private void ShowOverlay()
        {
            this.popup.IsOpen = true;
            this.LayoutRoot.Opacity = 0.6;
        }

        private void HideOverlay()
        {
            popup.IsOpen = false;
            this.LayoutRoot.Opacity = 1.0;
        }

        #endregion

        #region Navigation, Browser Load and Back Button Handlers
        void Browser_Navigating(object sender, NavigatingEventArgs e)
        {
            // Check for a special protocol. If there is, the browser control will handle it so don't show the progress bar.
            if (e.Uri.Scheme == "javascript")
            {
                return;
            }
            bool customProtocol = false;
            foreach (string s in launchProtocols)
            {
                if (e.Uri.OriginalString.StartsWith(s, StringComparison.OrdinalIgnoreCase))
                {
                    customProtocol = true;
                    break;
                }
            }

            if (!customProtocol) // If there's no special protocol...
            {
                // Do a quick check for the common case - if there's no file extension in the URL...
                if (e.Uri.Segments.Length > 0)
                {
                    // No file specified
                    if (!e.Uri.Segments[e.Uri.Segments.Length - 1].Contains('.'))
                    {
                        // Browse and show the progress bar
                        ShowOverlay();
                    }
                    //else // Otherwise check for file types
                    {
                        bool handledExtension = false;   // used when URL points to a common file type - skip the other checks
                        bool mediaFileType = false;  // used when URL points to a media file type - skip the other checks

                        // Check for common extensions that can be handled in the browser control
                        foreach (string s in bypassList)
                        {
                            if (e.Uri.OriginalString.EndsWith(s, StringComparison.OrdinalIgnoreCase))
                            {
                                ShowOverlay();
                                handledExtension = true;
                                break;
                            }
                        }
                        if (!handledExtension) // Are we launching a media file?
                        {
                            foreach (string s in mediaExtensions)
                            {
                                if (e.Uri.LocalPath.EndsWith(s, StringComparison.OrdinalIgnoreCase))
                                {
                                    MediaPlayerLauncher MPL = new MediaPlayerLauncher();
                                    MPL.Media = e.Uri;
                                    MPL.Show();
                                    mediaFileType = true;
                                    e.Cancel = true;
                                    HideOverlay();
                                    break;
                                }
                            }

                            if (!mediaFileType) // If not, are we launching another type of file?
                            {
                                foreach (string s in launchExtensions)
                                {
                                    if (e.Uri.OriginalString.EndsWith(s, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Windows.System.Launcher.LaunchUriAsync(e.Uri); // Launch the URI
                                        e.Cancel = true;
                                        HideOverlay();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!e.Cancel)
            {
                // Bug in WP8 SL WebBrowser control, it does not escape URI string before passing to external apps registered to handle
                // non-http protocols. 
                if (e.Uri.Scheme == "mailto" || e.Uri.Scheme == "tel")
                {
                    if (e.Uri.Scheme == "tel" && (_nonHttpUri == null || _nonHttpUri.OriginalString != e.Uri.OriginalString))
                    {
                        string trimmedUri = HttpUtility.UrlDecode(e.Uri.AbsoluteUri);
                        trimmedUri = trimmedUri.Replace(" ", String.Empty);
                        _nonHttpUri = new Uri(trimmedUri);
                        e.Cancel = true;
                        Browser.Navigate(_nonHttpUri);
                    }
                    HideOverlay();
                }
                else
                {
                    _currentUrl = e.Uri;
                }
            }
        }

        void Browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            _currentUrl = e.Uri;
            // Remove the progress bar
            HideOverlay();
        }


        private void Browser_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            // Remove the progress bar if we need to show an error message in the browser
            HideOverlay();

            if (e.Uri != null && e.Uri.Scheme != "javascript")
            {
                MessageBox.Show("An error has occured while loading new content. This could be due to network connectivity or server issue. Please check your network settings and try again.");
            }
        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // Remove the progress bar
            HideOverlay();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);

            // ... navigate through the browser's history
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
                e.Cancel = true; // if we skip this then the app will terminate.
            }
        }
        #endregion

        #region File and Media and Launch Extensions
        // If it's one of these, we'll need to launch the media player
        // from http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj207065(v=vs.105).aspx
        static string[] mediaExtensions = {
                ".3gp",
                ".3g2",
                ".3gpp",
                ".3gpp2",
                ".aac",
                ".aetx",
                ".asf",
                ".avi",
                ".m1v",
                ".m2v",
                ".m4a",
                ".m4r",
                ".m4v",
                ".mkv",
                ".mov",
                ".mp3",
                ".mp4",
                ".mpe",
                ".mpeg",
                ".mpg",
                ".qcp",
                ".wav",
                ".wdp",
                ".wma",
                ".wmv"};


        // If it's one of these, we'll need to launch the appropriate handler
        // from http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj207065(v=vs.105).aspx
        static string[] launchExtensions = {
                ".rtf",
                ".tif",
                ".tiff",
                ".one",
                ".onetoc2",
                ".doc",
                ".docm",
                ".docx",
                ".dot",
                ".dotm",
                ".dotx",
                ".pptx",
                ".pptm",
                ".potx",
                ".potm",
                ".ppam",
                ".ppsx",
                ".ppsm",
                ".ppt",
                ".pps",
                ".xls",
                ".xlm",
                ".xlt",
                ".xlsx",
                ".xlsm",
                ".xltx",
                ".xltm",
                ".xlsb",
                ".xlam",
                ".xll",
                ".cer",
                ".hdp",
                ".ico",
                ".icon",
                ".jxr",
                ".p7b",
                ".pem",
                ".txt",
                ".url",
                ".vcf",
                ".xap",
                ".xht",
                ".xsl",
                ".zip"};


        // IE will handle these protocols for us
        static string[] launchProtocols = {
                "callto",
                "mailto",
                "ms-",
                "onenote",
                "wallet",
                "zune"};


        // We can skip a bunch of logic if we see these common extensions
        static string[] bypassList = {
                ".com",
                ".htm",
                ".html",
                ".asp",
                ".aspx",
                ".js",
                ".xml"};
        #endregion


    }
}


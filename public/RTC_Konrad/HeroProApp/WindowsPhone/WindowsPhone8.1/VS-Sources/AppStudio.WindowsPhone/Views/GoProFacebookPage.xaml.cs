using System;
using System.Net.NetworkInformation;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class GoProFacebookPage : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public GoProFacebookPage()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            GoProFacebookModel = new GoProFacebookViewModel();
        }

        public GoProFacebookViewModel GoProFacebookModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);
            NavigationServices.CurrentViewModel = null;
            await GoProFacebookModel.LoadItems(NetworkInterface.GetIsNetworkAvailable());
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (GoProFacebookModel != null)
            {
                GoProFacebookModel.GetShareContent(args.Request);
            }
        }
    }
}

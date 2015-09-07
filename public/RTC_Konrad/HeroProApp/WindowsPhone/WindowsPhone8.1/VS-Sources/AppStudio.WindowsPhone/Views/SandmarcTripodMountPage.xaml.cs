using System;
using System.Net.NetworkInformation;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class SandmarcTripodMountPage : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public SandmarcTripodMountPage()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            SandmarcTripodMountModel = new SandmarcTripodMountViewModel();
        }

        public SandmarcTripodMountViewModel SandmarcTripodMountModel { get; private set; }

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
            await SandmarcTripodMountModel.LoadItems(NetworkInterface.GetIsNetworkAvailable());
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (SandmarcTripodMountModel != null)
            {
                SandmarcTripodMountModel.GetShareContent(args.Request);
            }
        }
    }
}

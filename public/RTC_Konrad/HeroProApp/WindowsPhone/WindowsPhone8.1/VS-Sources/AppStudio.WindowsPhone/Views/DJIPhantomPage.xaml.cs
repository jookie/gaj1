using System;
using System.Net.NetworkInformation;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class DJIPhantomPage : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public DJIPhantomPage()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            DJIPhantomModel = new DJIPhantomViewModel();
        }

        public DJIPhantomViewModel DJIPhantomModel { get; private set; }

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
            await DJIPhantomModel.LoadItems(NetworkInterface.GetIsNetworkAvailable());
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (DJIPhantomModel != null)
            {
                DJIPhantomModel.GetShareContent(args.Request);
            }
        }
    }
}

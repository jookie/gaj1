using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private AppViewModel _appModel;
       private GoProVODViewModel _goProVODModel;
       private GPTipsTricksViewModel _gPTipsTricksModel;
       private GoProPhotosViewModel _goProPhotosModel;
       private GoProMountsViewModel _goProMountsModel;
       private AboutViewModel _aboutModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = AppModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public AppViewModel AppModel
        {
            get { return _appModel ?? (_appModel = new AppViewModel()); }
        }
 
        public GoProVODViewModel GoProVODModel
        {
            get { return _goProVODModel ?? (_goProVODModel = new GoProVODViewModel()); }
        }
 
        public GPTipsTricksViewModel GPTipsTricksModel
        {
            get { return _gPTipsTricksModel ?? (_gPTipsTricksModel = new GPTipsTricksViewModel()); }
        }
 
        public GoProPhotosViewModel GoProPhotosModel
        {
            get { return _goProPhotosModel ?? (_goProPhotosModel = new GoProPhotosViewModel()); }
        }
 
        public GoProMountsViewModel GoProMountsModel
        {
            get { return _goProMountsModel ?? (_goProMountsModel = new GoProMountsViewModel()); }
        }
 
        public AboutViewModel AboutModel
        {
            get { return _aboutModel ?? (_aboutModel = new AboutViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            AppModel.ViewType = viewType;
            GoProVODModel.ViewType = viewType;
            GPTipsTricksModel.ViewType = viewType;
            GoProPhotosModel.ViewType = viewType;
            GoProMountsModel.ViewType = viewType;
            AboutModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

      get { return Visibility.Collapsed; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                AppModel.LoadItems(isNetworkAvailable),
                GoProVODModel.LoadItems(isNetworkAvailable),
                GPTipsTricksModel.LoadItems(isNetworkAvailable),
                GoProPhotosModel.LoadItems(isNetworkAvailable),
                GoProMountsModel.LoadItems(isNetworkAvailable),
                AboutModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// Refresh ViewModel items asynchronous
        /// </summary>
        public async Task RefreshData(bool isNetworkAvailable)
        {
            var refreshTasks = new Task[]
            { 
                AppModel.RefreshItems(isNetworkAvailable),
                GoProVODModel.RefreshItems(isNetworkAvailable),
                GPTipsTricksModel.RefreshItems(isNetworkAvailable),
                GoProPhotosModel.RefreshItems(isNetworkAvailable),
                GoProMountsModel.RefreshItems(isNetworkAvailable),
                AboutModel.RefreshItems(isNetworkAvailable),
            };
            await Task.WhenAll(refreshTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await RefreshData(NetworkInterface.GetIsNetworkAvailable());
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class GoProPhotosViewModel : ViewModelBase<FlickrSchema>
    {
        override protected string CacheKey
        {
            get { return "GoProPhotosDataSource"; }
        }

        override protected IDataSource<FlickrSchema> CreateDataSource()
        {
            return new GoProPhotosDataSource(); // FlickrDataSource
        }

        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("GoProPhotosDetail", "{Title}", "{Summary}", "{ImageUrl}");
        }

        override public Visibility ShareItemVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("GoProPhotosList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("GoProPhotosDetail");
        }
    }
}

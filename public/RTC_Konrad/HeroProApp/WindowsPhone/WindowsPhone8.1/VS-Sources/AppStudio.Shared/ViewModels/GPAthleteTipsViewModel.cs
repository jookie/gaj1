using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class GPAthleteTipsViewModel : ViewModelBase<YouTubeSchema>
    {
        override protected string CacheKey
        {
            get { return "GPAthleteTipsDataSource"; }
        }

        override protected IDataSource<YouTubeSchema> CreateDataSource()
        {
            return new GPAthleteTipsDataSource(); // YouTubeDataSource
        }

        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("GPAthleteTipsDetail", "{Title}", "{Summary}", "{ImageUrl}");
        }

        override public Visibility ShareItemVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public Visibility GoToSourceVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void GoToSource()
        {
            base.GoToSource("{ExternalUrl}");
        }

        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("GPAthleteTipsList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("GPAthleteTipsDetail");
        }
    }
}

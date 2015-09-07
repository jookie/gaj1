using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class GoProFacebookViewModel : ViewModelBase<FacebookSchema>
    {
        override protected string CacheKey
        {
            get { return "GoProFacebookDataSource"; }
        }

        override protected IDataSource<FacebookSchema> CreateDataSource()
        {
            return new GoProFacebookDataSource(); // FacebookDataSource
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
            NavigationServices.NavigateToPage("GoProFacebookList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("GoProFacebookDetail");
        }
    }
}

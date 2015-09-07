using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class AppViewModel : ViewModelBase<HtmlSchema>
    {
        override protected string CacheKey
        {
            get { return "AppDataSource"; }
        }

        override protected IDataSource<HtmlSchema> CreateDataSource()
        {
            return new AppDataSource(); // HtmlDataSource
        }

        override public bool IsStaticData
        {
            get { return true; }
        }

        override public ViewTypes ViewType
        {
            get { return ViewTypes.Detail; }
        }

        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("MainPage", "App", "{Content}", "");
        }
    }
}

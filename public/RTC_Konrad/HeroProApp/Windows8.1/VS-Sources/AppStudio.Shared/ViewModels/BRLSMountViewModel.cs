using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class BRLSMountViewModel : ViewModelBase<HtmlSchema>
    {
        override protected string CacheKey
        {
            get { return "BRLSMountDataSource"; }
        }

        override protected IDataSource<HtmlSchema> CreateDataSource()
        {
            return new BRLSMountDataSource(); // HtmlDataSource
        }

        override public bool IsStaticData
        {
            get { return true; }
        }

        override public ViewTypes ViewType
        {
            get { return ViewTypes.Detail; }
        }

        override public Visibility ShareItemVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}

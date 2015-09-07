using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class DJIPhantomViewModel : ViewModelBase<HtmlSchema>
    {
        override protected string CacheKey
        {
            get { return "DJIPhantomDataSource"; }
        }

        override protected IDataSource<HtmlSchema> CreateDataSource()
        {
            return new DJIPhantomDataSource(); // HtmlDataSource
        }

        override public bool IsStaticData
        {
            get { return true; }
        }

        override public ViewTypes ViewType
        {
            get { return ViewTypes.Detail; }
        }
    }
}

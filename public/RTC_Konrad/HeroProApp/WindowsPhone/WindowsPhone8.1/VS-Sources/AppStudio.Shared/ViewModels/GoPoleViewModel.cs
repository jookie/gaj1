using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class GoPoleViewModel : ViewModelBase<HtmlSchema>
    {
        override protected string CacheKey
        {
            get { return "GoPoleDataSource"; }
        }

        override protected IDataSource<HtmlSchema> CreateDataSource()
        {
            return new GoPoleDataSource(); // HtmlDataSource
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

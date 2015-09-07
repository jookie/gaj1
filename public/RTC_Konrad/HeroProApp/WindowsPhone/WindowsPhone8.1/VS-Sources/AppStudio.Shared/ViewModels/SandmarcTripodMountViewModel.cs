using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class SandmarcTripodMountViewModel : ViewModelBase<HtmlSchema>
    {
        override protected string CacheKey
        {
            get { return "SandmarcTripodMountDataSource"; }
        }

        override protected IDataSource<HtmlSchema> CreateDataSource()
        {
            return new SandmarcTripodMountDataSource(); // HtmlDataSource
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

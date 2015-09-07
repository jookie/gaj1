using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class RollPro3ViewModel : ViewModelBase<HtmlSchema>
    {
        override protected string CacheKey
        {
            get { return "RollPro3DataSource"; }
        }

        override protected IDataSource<HtmlSchema> CreateDataSource()
        {
            return new RollPro3DataSource(); // HtmlDataSource
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

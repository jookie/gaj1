using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace site2App.WP8
{
    public partial class Overlay : UserControl
    {
        public Overlay()
        {
            InitializeComponent();
            this.LayoutRoot.Height = Application.Current.Host.Content.ActualHeight; 
            this.LayoutRoot.Width = Application.Current.Host.Content.ActualWidth; 
        }
    }
}

using System;

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace AppStudio.Controls
{
    public sealed partial class WebControl : UserControl
    {
        public WebControl()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            this.SizeChanged += OnSizeChanged;


            webView.Width = Window.Current.Bounds.Width;
            webView.Height = Window.Current.Bounds.Height;

            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                this.webView.DefaultBackgroundColor = Windows.UI.Colors.Transparent;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            webView.DefaultBackgroundColor = Windows.UI.Colors.Transparent;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            webView.Width = e.NewSize.Width;
            webView.Height = e.NewSize.Height;
        }

        public string Html
        {
            get { return GetValue(HtmlProperty) as String; }
            set
            {
                if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                {
                    SetValue(HtmlProperty, value);
                }
            }
        }

        public string Source
        {
            get { return GetValue(SourceProperty) as String; }
            set { SetValue(SourceProperty, value); }
        }

        private void NavigateToString(string text)
        {
            webView.NavigateToString(text);
        }

        private void SetSource(string url)
        {
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                webView.Source = uri;
            }
        }

        private async void OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {

            if (args.Uri != null)
            {
                args.Cancel = true;
                var options = new LauncherOptions() { TreatAsUntrusted = false };
                await Windows.System.Launcher.LaunchUriAsync(args.Uri, options);
            }
        }

        static public readonly DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string), typeof(WebControl), new PropertyMetadata("", OnHtmlChanged));
        static public readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(WebControl), new PropertyMetadata("", OnSourceChanged));

        static private void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webControl = d as WebControl;

            string hexColor = "f0f0f0";
            SolidColorBrush colorBrush = webControl.Foreground as SolidColorBrush;
            if (colorBrush != null)
            {
                hexColor = colorBrush.Color.ToString().Substring(3);
            }

            string html = String.Format(HTML_PATTERN, hexColor, (e.NewValue as String) ?? String.Empty);

            webControl.NavigateToString(html);
        }

        static private void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string source = e.NewValue as String;
            var webControl = d as WebControl;
            webControl.SetSource(source);
        }

        const string HTML_PATTERN = "<!DOCTYPE html><html><head><style>body, html {{margin: 0px; padding: 0px; color: #{0}; font-family: Segoe UI; font-size: 1em; }}</style></head><body>{1}</body></html>";
    }
}

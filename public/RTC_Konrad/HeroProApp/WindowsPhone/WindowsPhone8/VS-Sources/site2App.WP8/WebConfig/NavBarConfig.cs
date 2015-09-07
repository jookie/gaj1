using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class NavBarConfig : AppBarConfig
    {
        public NavBarConfig()
        {
            IsEnabled = false;
            Buttons = new List<BarButton>();
        }
    }

    [DataContract]
    public class BarButton
    {
        public BarButton() { }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "iconUrl")]
        public string iconUrl { get; set; }

        public Uri IconUri { get { return new Uri(iconUrl, UriKind.Relative); } }

        public Uri Action { get { return new Uri(ActionString); } }

        [DataMember(Name = "action")]
        public string ActionString { get; set; }

    }
}

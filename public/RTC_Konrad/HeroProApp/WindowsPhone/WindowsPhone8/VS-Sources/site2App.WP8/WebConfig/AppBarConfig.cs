using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class AppBarConfig
    {
        public AppBarConfig()
        {
            IsEnabled = false;
            Buttons = new List<BarButton>();
        }
        [DataMember(Name = "enabled")]
        public bool IsEnabled { get; set; }

        [DataMember(Name = "buttons")]
        public List<BarButton> Buttons { get; set; }

        public void AddButton(string label, string icon, string action)
        {
            Buttons.Add(basicBtnSetup(label, icon, action));
        }

        public void AddButton(string label, string icon, string iconUri, string action)
        {
            BarButton nbb = basicBtnSetup(label, icon, action);
            Buttons.Add(nbb);
        }

        private BarButton basicBtnSetup(string label, string icon, string action)
        {
            BarButton nbb = new BarButton()
            {
                Label = label,
                Icon = icon,
                ActionString = action
            };
            return nbb;
        }
    }
}

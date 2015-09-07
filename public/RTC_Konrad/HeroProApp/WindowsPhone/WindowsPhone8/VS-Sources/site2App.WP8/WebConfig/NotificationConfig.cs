using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class NotificationConfig
    {
        public NotificationConfig() { }

        [DataMember(Name = "enabled")]
        public bool IsEnabled { get; set; }

        [DataMember(Name = "titlePollAddress")]
        public string TitlePollAddress { get; set; }

        [DataMember(Name = "TextField2")]
        public string TextField2 { get; set; }

    }
}

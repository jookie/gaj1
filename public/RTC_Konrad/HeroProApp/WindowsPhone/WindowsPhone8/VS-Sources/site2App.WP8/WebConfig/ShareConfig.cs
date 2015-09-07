using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class ShareConfig
    {
        public ShareConfig()
        {
            IsEnabled = false;
        }
        [DataMember(Name = "enabled")]
        public bool IsEnabled { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "url")]
        public string ShareUrl { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

    }
}

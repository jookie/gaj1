using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class RedirectConfig
    {
        public RedirectConfig()
        {
            IsEnabled = false;
            Links = new List<Link>();
        }

        [DataMember(Name = "enabled")]
        public bool IsEnabled { get; set; }

        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }
    }

    [DataContract]
    public class Link
    {
        public Link() { }

        public Uri LinkUri { get { return new Uri(LinkUriString); } }

        [DataMember(Name = "link")]
        public string LinkUriString { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }
    }
}

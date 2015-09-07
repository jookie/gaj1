using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class SearchConfig
    {
        public SearchConfig()
        {
            IsSearchEnabled = false;
        }

        [DataMember(Name = "enabled")]
        public bool IsSearchEnabled { get; set; }


        [DataMember(Name = "searchURL")]
        public string SearchUrlString { get; set; }
        public Uri SearchUrl { get { return new Uri(SearchUrlString); } }

    }
}

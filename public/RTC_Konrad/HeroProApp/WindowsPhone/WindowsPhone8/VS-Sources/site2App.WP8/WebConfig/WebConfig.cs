using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
        [DataContract]
        public class WebConfig
        {
            public WebConfig() { }

            public WebConfig(string jsonConfigFile)
            {
            }


            public Uri BaseURL { get { return new Uri(BaseURLString); } }

            [DataMember(Name = "baseURL")]
            public string BaseURLString { get; set; }

            public Uri MobileBaseURL { get { return new Uri(MobileURLString); } }

            [DataMember(Name = "mobileURL")]
            public string MobileURLString { get; set; }

            [DataMember(Name = "share")]
            public ShareConfig Share { get; set; }

            [DataMember(Name = "appBar")]
            public AppBarConfig AppBar { get; set; }

            [DataMember(Name = "navBar")]
            public NavBarConfig NavBar { get; set; }

            // Future implementation stub
            //[DataMember(Name = "livetile")]
            //public LiveTileConfig LiveTile { get; set; }

            [DataMember(Name = "notifications")]
            public NotificationConfig Notifications { get; set; }

            [DataMember(Name = "redirects")]
            public RedirectConfig Redirects { get; set; }

            [DataMember(Name = "settings")]
            public SettingsConfig Settings { get; set; }

            [DataMember(Name = "styles")]
            public StylesConfig Styles { get; set; }

            [DataMember(Name = "search")]
            public SearchConfig Search { get; set; }

            [DataMember(Name = "styleTheme")]
            public string StyleTheme { get; set; }

            public static WebConfig CreateConfig(string sourceJSON)
            {
                return JsonConvert.DeserializeObject<WebConfig>(sourceJSON);
            }

            public static string CreateJson(WebConfig config)
            {
                return JsonConvert.SerializeObject(config);
            }
            private bool getJSONBool(string boolVal)
            {
                return (boolVal == "true");
            }

        }
    

}

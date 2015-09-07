using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.Config
{
    [DataContract]
    public class SettingsConfig
    {
        public SettingsConfig()
        {
            IsEnabled = false;
        }
        [DataMember(Name = "enabled")]
        public bool IsEnabled { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "header")]
        public string Header { get; set; }

        [DataMember(Name = "HTMLText1")]
        public string HTMLText1 { get; set; }

        [DataMember(Name = "HTMLText2")]
        public string HTMLText2 { get; set; }

        [DataMember(Name = "HTMLText3")]
        public string HTMLText3 { get; set; }

        [DataMember(Name = "feedbackEmail")]
        public string FeedbackEmail { get; set; }

        [DataMember(Name = "feedbackSubject")]
        public string FeedbackSubject { get; set; }

        [DataMember(Name = "feedbackUrl")]
        public string FeedbackUrl { get; set; }

        public Uri PrivacyUrl { get { return new Uri(PrivacyUrlString); } }
        [DataMember(Name = "privacyUrl")]
        public string PrivacyUrlString { get; set; }

        public Uri SettingsUrl { get { return new Uri(SettingsUrlString); } }
        [DataMember(Name = "settingsUrl")]
        public string SettingsUrlString { get; set; }
    }
}

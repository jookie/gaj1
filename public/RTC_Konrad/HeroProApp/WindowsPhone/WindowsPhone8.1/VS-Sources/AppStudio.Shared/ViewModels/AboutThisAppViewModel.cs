using System;

using Windows.ApplicationModel;

namespace AppStudio.ViewModels
{
    public class AboutThisAppViewModel
    {
        public string Publisher
        {
            get
            {
                return Package.Current.Id.Publisher.Substring(3);
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor);
            }
        }

        public string AboutText
        {
            get
            {
                return "Control, View, Share, Enjoy!\n\nHeroProApp, the 100% Free, OpenSource, GitHub-based" +
    " HTML5 App to control GoPro Cameras.\n\nFor HERO2 w/ Wifi BacPac, HERO3 Silver, HE" +
    "RO3 Black, HERO3+Black.\n\nMake sure you have 3G & the GoPro Wifi is connected to " +
    "the phone.";
            }
        }

        public string LicenseText
        {
            get
            {
                return @"<p><b>Important read carefully</b></p>" +
                          @"This End-User License Agreement (""EULA"") is a legal agreement " +
                          @"between you (either an individual or a single entity) and " +
                          @"Microsoft Corporation for the Microsoft software that accompanies " +
                          @"this EULA, which includes associated media and Microsoft Internet-based " +
                          @"services (""Software""). <br/>" +
                          @"An amendment or addendum to this EULA may accompany the Software. " +
                          @"<p><b>You agree to be bound by the terms of this EULA by installing, copying, " +
                          @"or using the software. If you do not agree, do not install, copy, or use the " +
                          @"software; you may return it to your place of purchase for a full refund, if applicable.</b></p>";
            }
        }
    }
}


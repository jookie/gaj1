using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace site2App.WP8
{

    public class ShareItem
    {
        public string Target
        {
            get;
            set;
        }
        public ShareItem(string target)
        {
            this.Target = target;
        }
    }
    public partial class SharePage : PhoneApplicationPage
    {
        private string link;

        public SharePage()
        {
            InitializeComponent();
            List<ShareItem> options = new List<ShareItem>();
            options.Add(new ShareItem("Email"));
            options.Add(new ShareItem("Messages"));
            options.Add(new ShareItem("Social networks"));
            ListSelector.ItemsSource = options;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("LinkURL", out link);
            base.OnNavigatedTo(e);
        }

        private void ListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ShareItem)((LongListSelector)sender).SelectedItem).Target)
            {
                case "Email":
                    {
                        EmailComposeTask ect = new EmailComposeTask();
                        ect.Body = link;
                        ect.Show();
                        break;
                    }
                case "Messages":
                    {
                        SmsComposeTask sct = new SmsComposeTask();
                        sct.Body = link;
                        sct.Show();
                        break;
                    }
                case "Social networks":
                    {
                        ShareLinkTask slt = new ShareLinkTask();
                        slt.LinkUri = new Uri(link);
                        slt.Show();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}

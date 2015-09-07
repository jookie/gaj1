using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Xml.Linq;

namespace AppStudio.Data
{
    public class FacebookDataProvider
    {
        private const string USER_RSS_URL = "https://www.facebook.com/feeds/page.php?id={0}&format=rss20";
        private Uri _uri;

        public FacebookDataProvider(string userID)
        {
            _uri = new Uri(string.Format(USER_RSS_URL, userID));
        }

        public async Task<ObservableCollection<FacebookSchema>> Load()
        {
            var rssProvider = new RssDataProvider(_uri.ToString(), "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");

            var rssSchemaList = await rssProvider.Load();
            var result = new ObservableCollection<FacebookSchema>
                (
                    rssSchemaList.Select(rss => new FacebookSchema()
                    {
                        Author = rss.Author,
                        Content = rss.Content,
                        FeedUrl = rss.FeedUrl,
                        Id = rss.Id,
                        ImageUrl = rss.ImageUrl,
                        PublishDate = rss.PublishDate,
                        Summary = rss.Summary,
                        Title = rss.Title
                    })
                    .OrderByDescending(f => f.PublishDate)
                );

            return result;
        }
    }
}

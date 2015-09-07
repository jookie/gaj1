using System;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppStudio.Data
{
    /// <summary>
    /// YouTube data provider class
    /// </summary>
    public class YouTubeDataProvider
    {
        private Uri _uri;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url"></param>
        public YouTubeDataProvider(string url)
        {
            _uri = new Uri(url);
        }

        /// <summary>
        /// Starts loading the feed and parse the response.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<YouTubeSchema>> Load()
        {
            string xmlContent = await DownloadAsync(_uri);
            var atoms = XNamespace.Get("http://www.w3.org/2005/Atom");
            var medians = XNamespace.Get("http://search.yahoo.com/mrss/");

            var doc = XDocument.Parse(xmlContent);

            var result = (from entry in doc.Descendants(atoms.GetName("entry"))
                          select new YouTubeSchema()
                          {
                              Title = GetYouTubeTitle(atoms, entry, medians),
                              Summary = GetYouTubeSummary(atoms, entry, medians),
                              VideoUrl = GetVideoUrl(atoms, entry),
                              ImageUrl = entry.Descendants(medians.GetName("thumbnail")).Select(thumbnail => thumbnail.Attribute("url").Value).FirstOrDefault()
                          }).ToArray();

            foreach (var item in result)
            {
                item.MediaUrl = (await YouTube.GetVideoUriAsync(item.VideoId, YouTubeQuality.Unknown)).Uri.ToString();
            }

            return result;
        }

        private async Task<string> DownloadAsync(Uri url)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(url.AbsoluteUri);
        }

        private string GetVideoUrl(XNamespace atoms, XElement entry)
        {
            string videoUrl = string.Empty;
            string id = string.Empty;
            if (entry.Element(atoms.GetName("id")) != null && entry.Element(atoms.GetName("id")).Value != null)
                id = entry.Element(atoms.GetName("id")).Value.Split(':').Last();

            videoUrl = String.Format(CultureInfo.InvariantCulture, "http://gdata.youtube.com/feeds/api/videos/{0}", id);

            return videoUrl;
        }

        private string GetYouTubeTitle(XNamespace atoms, XElement entry, XNamespace medians)
        {
            string title = string.Empty;

            if (entry.Element(atoms.GetName("title")) != null)
                title = entry.Element(atoms.GetName("title")).Value;

            return title;
        }

        private string GetYouTubeSummary(XNamespace atoms, XElement entry, XNamespace medians)
        {
            string summary = string.Empty;
            if (entry.Element(atoms.GetName("summary")) != null)
                summary = entry.Element(atoms.GetName("summary")).Value;

            if (string.IsNullOrEmpty(summary))
                if (entry.Element(atoms.GetName("content")) != null)
                    summary = entry.Element(atoms.GetName("content")).Value;

            if (string.IsNullOrEmpty(summary))
                if (entry.Element(medians.GetName("group")) != null && entry.Element(medians.GetName("group")).Element(medians.GetName("description")) != null)
                    summary = entry.Element(medians.GetName("group")).Element(medians.GetName("description")).Value;

            return summary;
        }
    }
}

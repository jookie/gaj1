using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class AppDataSource : IDataSource<HtmlSchema>
    {
        private IEnumerable<HtmlSchema> _data = new HtmlSchema[]
        {
            new HtmlSchema
            {
                Id = "{8def980c-12b0-4646-989a-dda7b11bd136}",
                Content = @"Make sure you are connected to the GoPro Wifi and you have 3G/4G
<br><br>HeroProApp works on the browser.
<br><br><a rel=""nofollow"" target=""_blank"" href=""http://m.heropro.chernowii.com""><span class=""wysiwyg-font-size-larger"">Go to HeroProApp in Browser</span></a>
<embed src=""http://m.heropro.chernowii.com"" width=""100%"" height=""100%""/>"
            }
        };

        public async Task<IEnumerable<HtmlSchema>> LoadData()
        {
            return await Task.Run(() =>
            {
                return _data;
            });
        }

        public async Task<IEnumerable<HtmlSchema>> Refresh()
        {
            return await LoadData();
        }
    }
}

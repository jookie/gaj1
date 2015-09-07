using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class TheBobberDataSource : IDataSource<HtmlSchema>
    {
        private IEnumerable<HtmlSchema> _data = new HtmlSchema[]
        {
            new HtmlSchema
            {
                Id = "{6a144a6d-e322-4345-8edb-404effd9a79d}",
                Content = "This yellow tube does much more than you think, shoot yourself surfing&nbsp;those" +
    " crystal water blue&nbsp;waves or shoot the snow hitting your face.<br><br><br><" +
    "a href=\"http://gopole.com/products/the-bobber\">Go to the bobber page</a>"
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

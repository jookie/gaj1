using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class GoPoleDataSource : IDataSource<HtmlSchema>
    {
        private IEnumerable<HtmlSchema> _data = new HtmlSchema[]
        {
            new HtmlSchema
            {
                Id = "{e0db3fe7-9337-4033-91bd-9553ff8e5798}",
                Content = "The GoPole allows you to capture yourself jumping that massive cliff or hitting t" +
    "he slopes<br><br><br><a href=\"http://gopole.com\" target=\"_blank\">GoPole.com</a>"
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

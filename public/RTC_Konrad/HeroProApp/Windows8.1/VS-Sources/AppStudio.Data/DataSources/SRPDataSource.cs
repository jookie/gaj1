using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class SRPDataSource : IDataSource<HtmlSchema>
    {
        private IEnumerable<HtmlSchema> _data = new HtmlSchema[]
        {
            new HtmlSchema
            {
                Id = "{67092c13-1452-4e47-ae84-4a3fa0dfb205}",
                Content = "SRP: Filters, lenses, trays and accesories for all the HERO cameras, great for un" +
    "derwater color correction and for blur.<br><br><br><a href=\"http://snakeriverpro" +
    "totyping.com\">Go to SnakeRiverPrototyping.com</a>"
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

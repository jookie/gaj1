using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class DJIPhantomDataSource : IDataSource<HtmlSchema>
    {
        private IEnumerable<HtmlSchema> _data = new HtmlSchema[]
        {
            new HtmlSchema
            {
                Id = "{a6e77a51-3e05-4b28-b26a-4d1543268c72}",
                Content = "The DJI Phantom is a quadcopter that can carry a GoPro camera and a gimbal, the p" +
    "hantom is great for aerial shots and it is widely used in TV.<br><br><br><a href" +
    "=\"http://dji.com\">Go to DJI.com</a>"
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class EasyGimbalDataSource : IDataSource<HtmlSchema>
    {
        private IEnumerable<HtmlSchema> _data = new HtmlSchema[]
        {
            new HtmlSchema
            {
                Id = "{1720e349-0f4b-4a90-a187-7592852502d4}",
                Content = "The Easy Gimbal allows you to get those smooth shots you\'ve dreaming!<br><br><br>" +
    "<a href=\"http://easygimbal.com\">Go to EasyGimbal.com</a>"
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

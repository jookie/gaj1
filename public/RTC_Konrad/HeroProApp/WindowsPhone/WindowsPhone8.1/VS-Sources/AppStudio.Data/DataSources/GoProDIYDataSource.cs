using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class GoProDIYDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url = @"https://gdata.youtube.com/feeds/api/users/DIYGoProVideo/uploads?start-index=1&max-results=20&v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public GoProDIYDataSource()
        {
        }

        public async Task<IEnumerable<YouTubeSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var youTubeDataProvider = new YouTubeDataProvider(_url);
                    _data = await youTubeDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("GoProDIYDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<YouTubeSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}

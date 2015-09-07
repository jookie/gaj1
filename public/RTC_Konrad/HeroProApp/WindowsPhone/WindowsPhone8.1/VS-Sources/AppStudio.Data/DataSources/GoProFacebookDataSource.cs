using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class GoProFacebookDataSource : IDataSource<FacebookSchema>
    {
        private const string _userID = "50043151918";

        private IEnumerable<FacebookSchema> _data = null;

        public GoProFacebookDataSource()
        {
        }

        public async Task<IEnumerable<FacebookSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var facebookDataProvider = new FacebookDataProvider(_userID);
                    _data = await facebookDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("GoProFacebookDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<FacebookSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}

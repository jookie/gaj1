using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class GPTipsTricksDataSource : IDataSource<MenuSchema>
    {
        private readonly IEnumerable<MenuSchema> _data = new MenuSchema[]
		{
            new MenuSchema
            {
                Type = "Section",
                Title = "GoPro Tips",
                Icon = "/Assets/DataImages/gptips.png",
                Target = "GoProTipsPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "GP Athlete Tips",
                Icon = "/Assets/DataImages/gpathlete.png",
                Target = "GPAthleteTipsPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "GoPro DIY",
                Icon = "/Assets/DataImages/GPDIY.png",
                Target = "GoProDIYPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "GoPro Facebook",
                Icon = "/Assets/DataImages/facebooklogo.png",
                Target = "GoProFacebookPage",
            },
		};

        public async Task<IEnumerable<MenuSchema>> LoadData()
        {
            return await Task.Run(() =>
            {
                return _data;
            });
        }

        public async Task<IEnumerable<MenuSchema>> Refresh()
        {
            return await LoadData();
        }
    }
}

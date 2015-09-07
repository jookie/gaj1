using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class GoProMountsDataSource : IDataSource<MenuSchema>
    {
        private readonly IEnumerable<MenuSchema> _data = new MenuSchema[]
		{
            new MenuSchema
            {
                Type = "Section",
                Title = "BRLS Mount",
                Icon = "/Assets/DataImages/brls.png",
                Target = "BRLSMountPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "GoPole",
                Icon = "/Assets/DataImages/gopole.png",
                Target = "GoPolePage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "RollPro 3 ",
                Icon = "/Assets/DataImages/rollpro.png",
                Target = "RollPro3Page",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "GoFlex-Arm",
                Icon = "/Assets/DataImages/goflexarm.png",
                Target = "GoFlexArmPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "PolarPro",
                Icon = "/Assets/DataImages/polarpro.png",
                Target = "PolarProPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Original Handle",
                Icon = "/Assets/DataImages/originalhandle.png",
                Target = "OriginalHandlePage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "SRP",
                Icon = "/Assets/DataImages/blurfixsrp.png",
                Target = "SRPPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "DJI Phantom",
                Icon = "/Assets/DataImages/phantomdji.png",
                Target = "DJIPhantomPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Easy Gimbal",
                Icon = "/Assets/DataImages/easygimbal.png",
                Target = "EasyGimbalPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "The Bobber",
                Icon = "/Assets/DataImages/bobber.png",
                Target = "TheBobberPage",
            },
            new MenuSchema
            {
                Type = "Section",
                Title = "Sandmarc Tripod Mount",
                Icon = "/Assets/DataImages/sandmarctripod.png",
                Target = "SandmarcTripodMountPage",
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

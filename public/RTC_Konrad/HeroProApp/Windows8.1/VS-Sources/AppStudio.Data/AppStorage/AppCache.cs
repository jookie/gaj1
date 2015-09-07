using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace AppStudio.Data
{
    public class AppCache
    {
        static public async Task<T> GetItem<T>(string key) where T : BindableSchemaBase
        {
            string json = await UserStorage.ReadTextFromFile(key);
            if (!String.IsNullOrEmpty(json))
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("AppCache.GetItem", ex);
                }
                await UserStorage.DeleteFileIfExists(key);
            }
            return null;
        }

        static public async Task AddItem<T>(string key, T item) where T : BindableSchemaBase
        {
            try
            {
                string json = JsonConvert.SerializeObject(item);
                await UserStorage.WriteText(key, json);
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("AppCache.AddItem", ex);
            }
        }

        static public async Task<IEnumerable<T>> GetItems<T>(string key) where T : BindableSchemaBase
        {
            string json = await UserStorage.ReadTextFromFile(key);
            if (!String.IsNullOrEmpty(json))
            {
                try
                {
                    IEnumerable<T> records = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                    return records;
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("AppCache.GetItems", ex);
                }
                await UserStorage.DeleteFileIfExists(key);
            }
            return null;
        }

        static public async Task AddItems<T>(string key, IEnumerable<T> items) where T : BindableSchemaBase
        {
            try
            {
                string json = JsonConvert.SerializeObject(items);
                await UserStorage.WriteText(key, json);
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("AppCache.AddItems", ex);
            }
        }
    }
}

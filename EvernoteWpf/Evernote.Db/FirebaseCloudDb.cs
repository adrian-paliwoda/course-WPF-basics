using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Evernote.Model.Interfaces;
using Newtonsoft.Json;

namespace Evernote.Db
{
    public class FirebaseCloudDb
    {
        private const string FirebaseDatabaseUrl = "https://evernotewpf-default-rtdb.firebaseio.com/";

        public static bool Insert<T>(T item)
        {
            if (item != null)
            {
                var body = JsonConvert.SerializeObject(item);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var url = $"{FirebaseDatabaseUrl}{item.GetType().Name.ToLower()}.json";

                using (var client = new HttpClient())
                {
                    var respond = client.PostAsync(url, content).Result;

                    if (respond.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static async Task<bool> Update<T>(T item) where T : IHasId
        {
            if (item != null)
            {
                var body = JsonConvert.SerializeObject(item);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var url = $"{FirebaseDatabaseUrl}{item.GetType().Name.ToLower()}/{item.Id}.json";

                using (var client = new HttpClient())
                {
                    var respond = client.PatchAsync(url, content).Result;

                    if (respond.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static async Task<bool> Delete<T>(T item) where T : IHasId
        {
            if (item != null)
            {
                var url = $"{FirebaseDatabaseUrl}{item.GetType().Name.ToLower()}/{item.Id}.json";

                using (var client = new HttpClient())
                {
                    var respond = await client.DeleteAsync(url);

                    if (respond.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static async Task<List<T>> Read<T>() where T : IHasId
        {
            var url = $"{FirebaseDatabaseUrl}{typeof(T).Name.ToLower()}.json";
        
            using (var client = new HttpClient())
            {
                var respond =  client.GetAsync(url).Result;
                if (respond.IsSuccessStatusCode)
                {
                    var result = await respond.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<Dictionary<string, T>>(result);
        
                    if (items is {Count: > 0})
                    {
                        var list = new List<T>();
                        foreach (var item in items)
                        {
                            item.Value.Id = item.Key;
                            list.Add(item.Value);
                        }

                        return list;
                    }
                }
            }
        
            return new List<T>();
        }
        
    }
}
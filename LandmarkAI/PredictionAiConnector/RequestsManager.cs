using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PredictionAiConnector
{
    public static class RequestsManager
    {
        public static async Task<IList<Prediction>?> MakePredictionAsync(string imagePath)
        {
            var imageFile = await File.ReadAllBytesAsync(imagePath);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Prediction-Key", Consts.PredictionKey);
                using (var content = new ByteArrayContent(imageFile))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(Consts.ContentType);
                    var respond = await httpClient.PostAsync(Consts.Url, content);
                    respond.EnsureSuccessStatusCode();

                    var result = await respond.Content.ReadAsStringAsync();
                    return  (JsonConvert.DeserializeObject<CustomVision>(result))?.Predictions;
                }
            }
        }
    }
}
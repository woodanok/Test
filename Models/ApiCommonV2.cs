using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomrsFinder.Models
{
    public class ApiCommonV2
    {
        private readonly static String Uri = "URI";

        public static async Task<T> GetAsync<T>(String method, String token = null, Dictionary<String, String> getParams = null)
        {
            var get = getParams is null ? null : $"?{String.Join("&", getParams.Select(s => $"{s.Key}={s.Value}"))}";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var streamTask = client.GetStreamAsync(Uri + method + get);
            var repositories = await JsonSerializer.DeserializeAsync<T>(await streamTask);
            return repositories;
        }

        public static async Task<T> PostAsync<T>(String method, Object model = null, String token = null)
        {
            String json = JsonSerializer.Serialize(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsync(Uri + method, content);
            HttpContent responseContent = response.Content;
            var responceString = await responseContent.ReadAsStringAsync();
            var repositories = JsonSerializer.Deserialize<T>(responceString);
            return repositories;
        }
    }
}

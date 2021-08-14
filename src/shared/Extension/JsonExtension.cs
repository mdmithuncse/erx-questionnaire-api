using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Extension
{
    public static class JsonExtension
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            IgnoreNullValues = false
        };

        public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(contentStream,
                Options);
            return result;
        }

        public static T Deserialize<T>(string content)
        {
            var result = JsonSerializer.Deserialize<T>(content, Options);
            return result;
        }

        public static string Serialize<T>(T content)
        {
            return JsonSerializer.Serialize(content,
                Options);
        }
    }
}

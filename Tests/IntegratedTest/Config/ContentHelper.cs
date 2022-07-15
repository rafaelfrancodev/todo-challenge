using Infra.CrossCutting.Controllers;
using Newtonsoft.Json;
using System.Text;


namespace IntegratedTest.Config
{
    public static class ContentHelper<T>
    {
        public static StringContent FormatStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }

        public static async Task<Result<T>> GetResponse(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<T>>(stringResponse);
            return result;
        }
    }
}

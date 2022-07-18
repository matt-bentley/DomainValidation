using DomainValidation.Api.Controllers.Abstract;
using Newtonsoft.Json;

namespace DomainValidation.Api.Tests.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAndAssertSuccessAsync<T>(this HttpResponseMessage response) where T : class
        {
            response.IsSuccessStatusCode.Should().BeTrue();
            var json = await response.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string))
            {
                return json as T;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public static async Task<Envelope> ReadAndAssertBadRequestAsync(this HttpResponseMessage response)
        {
            return await ReadAndAssertResponseAsync(response, HttpStatusCode.BadRequest);
        }

        public static async Task<Envelope> ReadAndAssertNotFoundAsync(this HttpResponseMessage response)
        {
            return await ReadAndAssertResponseAsync(response, HttpStatusCode.NotFound);
        }

        public static async Task<Envelope> ReadAndAssertResponseAsync(this HttpResponseMessage response, HttpStatusCode httpStatusCode)
        {
            response.StatusCode.Should().Be(httpStatusCode);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Envelope>(json, new JsonSerializerSettings() { ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor });
        }
    }
}

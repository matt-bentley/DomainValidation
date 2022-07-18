using DomainValidation.Api.Tests.Extensions;
using DomainValidation.Application.ReadModels;
using DomainValidation.Application.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace DomainValidation.Api.Tests
{
    [TestClass]
    public class CustomersControllerTests
    {
        private readonly WebApplicationFactory<Program> _factory = new WebApplicationFactory<Program>();
        private readonly HttpClient _client;
        private const string BASE_URL = "api/Customers";

        public CustomersControllerTests()
        {
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenGetWithNone_ThenReturnEmpty()
        {
            await AssertNoCustomersAsync();
        }

        private async Task AssertNoCustomersAsync()
        {
            var response = await _client.GetAsync(BASE_URL);
            var customers = await response.ReadAndAssertSuccessAsync<List<CustomerReadModel>>();
            customers.Should().HaveCount(0);
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenGetOne_ThenReturnOne()
        {
            await AddTestUserAsync();
            var response = await _client.GetAsync(BASE_URL);

            var customers = await response.ReadAndAssertSuccessAsync<List<CustomerReadModel>>();
            customers.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenGetById_ThenOk()
        {
            var created = await GetTestUserAsync();

            var response = await _client.GetAsync($"{BASE_URL}/{created.Id}");

            var customer = await response.ReadAndAssertSuccessAsync<CustomerReadModel>();
            customer.FirstName.Should().Be("Joe");
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenInvalidGetById_ThenNotFound()
        {
            var response = await _client.GetAsync($"{BASE_URL}/{Guid.NewGuid()}");
            var result = await response.ReadAndAssertNotFoundAsync();
            result.ErrorMessage.Should().Be("Record not found");
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenInsert_ThenCreatedResult()
        {
            var response = await AddTestUserAsync();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenInsertDuplicate_ThenBadRequest()
        {
            await AddTestUserAsync();
            var response = await AddTestUserAsync();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenInsertInvalid_ThenBadRequest()
        {
            var request = new CreateCustomerRequest()
            {
                Email = "tester@test.com",
                FirstName = "",
                LastName = "Bloggs"
            };

            var response = await _client.PostAsync(BASE_URL, GetBody(request));
            var result = await response.ReadAndAssertBadRequestAsync();
            result.ErrorMessage.Should().Be("First Name is required");
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenDelete_ThenNoContent()
        {
            var created = await GetTestUserAsync();

            var response = await _client.DeleteAsync($"{BASE_URL}/{created.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            await AssertNoCustomersAsync();
        }

        [TestMethod]
        public async Task GivenCustomersController_WhenDeleteInvalid_ThenNotFound()
        {
            var response = await _client.DeleteAsync($"{BASE_URL}/{Guid.NewGuid()}");

            await response.ReadAndAssertNotFoundAsync();
        }

        private async Task<CreatedReadModel> GetTestUserAsync()
        {
            var createdResponse = await AddTestUserAsync();
            return await createdResponse.ReadAndAssertSuccessAsync<CreatedReadModel>();
        }

        private async Task<HttpResponseMessage> AddTestUserAsync()
        {
            var request = new CreateCustomerRequest()
            {
                Email = "tester@test.com",
                FirstName = "Joe",
                LastName = "Bloggs"
            };

            return await _client.PostAsync(BASE_URL, GetBody(request));
        }

        private static StringContent GetBody<T>(T item)
        {
            return new StringContent(JsonSerializer.Serialize(item, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }), Encoding.UTF8, "application/json");
        }
    }
}
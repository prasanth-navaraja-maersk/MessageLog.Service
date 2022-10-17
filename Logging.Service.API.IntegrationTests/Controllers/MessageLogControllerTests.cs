using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using Logging.Service.API.IntegrationTests.TestFramework;

namespace Logging.Service.API.IntegrationTests.Controllers
{
    public class MessageLogControllerTests
    {
        private readonly Faker _faker;

        public MessageLogControllerTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public async Task Upsert_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var api = new ApiWebApplicationFactory();
            var messageLog = (new JsonObject
            {
                ["MessageId"] = _faker.Random.AlphaNumeric(10),
                ["StandardAlphaCarrierCode"] = _faker.Random.AlphaNumeric(5),
                ["CustomerCode"] = _faker.Random.AlphaNumeric(4),
                ["VendorName"] = _faker.Random.AlphaNumeric(10),
                ["InvoiceNumber"] = _faker.Random.AlphaNumeric(10),
                ["Status"] = _faker.Random.AlphaNumeric(10),
                ["IsError"] = _faker.Random.Bool(),
                ["Stage"] = _faker.Random.AlphaNumeric(10),
                ["Source"] = _faker.Random.AlphaNumeric(10),
                ["Destination"] = _faker.Random.AlphaNumeric(10),
            }).ToJsonString();

            using var msgLogs = JsonDocument.Parse(messageLog);
            var messageLogRequest = new MessageLogRequest
            {
                MessageId = _faker.Random.AlphaNumeric(10),
                MessageType = _faker.Random.AlphaNumeric(10),
                MessageLogs = msgLogs
            };

            // Act
            var result = await api.CreateClient()
                .PostAsJsonAsync("/MessageLogs", messageLogRequest, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }
    }
}

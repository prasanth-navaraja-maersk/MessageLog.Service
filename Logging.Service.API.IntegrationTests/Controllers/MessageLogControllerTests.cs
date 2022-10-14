using System.Net.Http.Headers;
using System.Net.Http.Json;
using Logging.Service.API.Controllers;
using Logging.Service.Application;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
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
            var messageLogHandler = Mock.Of<IMessageLogHandler>();
            var messageLogController = new MessageLogController(Mock.Of<ILogger<MessageLogController>>(), messageLogHandler);
            var testData = (new JsonObject
            {
                ["Date"] = new DateTime(2019, 8, 1),
                ["Temperature"] = 25,
                ["Summary"] = "Hot",
                ["DatesAvailable"] = new JsonArray(
                    new DateTime(2019, 8, 1), new DateTime(2019, 8, 2)),
                ["TemperatureRanges"] = new JsonObject
                {
                    ["Cold"] = new JsonObject
                    {
                        ["High"] = 20,
                        ["Low"] = -10
                    }
                },
                ["SummaryWords"] = new JsonArray("Cool", "Windy", "Humid")
            }).ToJsonString();

            using var msgLogs = JsonDocument.Parse(testData);
            var messageLogRequest = new MessageLogRequest
            {
                MessageId = _faker.Random.AlphaNumeric(10),
                MessageType = _faker.Random.AlphaNumeric(10),
                MessageLogs = msgLogs
            };

            // Act
            //var result = await messageLogController.Upsert(
            //    messageLogRequest,
            //    CancellationToken.None);
            //var result = await api.CreateClient()
            //    .PostAsync("/MessageLogs/Upsert", messageLogRequest, CancellationToken.None).Result;
            var result = await api.CreateClient()
                .PostAsJsonAsync("/MessageLogs", messageLogRequest, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }
    }
}

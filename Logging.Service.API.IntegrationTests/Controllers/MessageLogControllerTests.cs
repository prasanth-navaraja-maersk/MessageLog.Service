using Logging.Service.API.Controllers;
using Logging.Service.Application;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Xunit;
using Bogus;

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
            //var controller = new MessageLogController(Mock.Of<Logger<MessageLogController>>(), Mock.Of<IMessageLogHandler>());
            var messageLogController = new MessageLogController(Mock.Of<Logger<MessageLogController>>(), Mock.Of<IMessageLogHandler>());
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
            var result = await messageLogController.Upsert(
                messageLogRequest,
                CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task TestTestTest()
        {
            await using var application = new WebApplicationFactory<Program>();

        }
    }
}

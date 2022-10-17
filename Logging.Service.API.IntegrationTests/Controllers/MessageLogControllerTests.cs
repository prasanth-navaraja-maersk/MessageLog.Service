﻿using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using Logging.Service.API.IntegrationTests.TestFramework;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace Logging.Service.API.IntegrationTests.Controllers
{
    public class MessageLogControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _fixture;
        private readonly Faker _faker;

        public MessageLogControllerTests(ApiWebApplicationFactory fixture)
        {
            _fixture = fixture;
            _faker = new Faker();
        }

        [Fact]
        public async Task Upsert_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
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
            //var result = await _fixture.CreateClient()
            //    .PostAsJsonAsync("/MessageLogs", messageLogRequest, CancellationToken.None);

            //// Assert
            //result.Should().NotBeNull();
            //result.StatusCode.Should().Be(HttpStatusCode.OK);

            var step = Step.Create("Upsert_Message_Logs", async _ =>
            {
                var resp = await _fixture.CreateClient()
                    .PostAsJsonAsync("/MessageLogs", messageLogRequest, CancellationToken.None);

                return resp.ToNBomberResponse();
            });

            var scenario = ScenarioBuilder
                .CreateScenario("Message_Logs", step)
                .WithoutWarmUp()
                .WithLoadSimulations(
                    Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(10)));

            var stats = NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFormats(ReportFormat.Html, ReportFormat.Md)
                .Run();

            // Assert
            stats.Should().NotBeNull();
            var stepStats = stats.ScenarioStats[0].StepStats[0];

            stepStats.Ok.Request.Count.Should().BeGreaterThan(1000);
            stepStats.Ok.Request.RPS.Should().BeGreaterThan(100);
            stepStats.Ok.Latency.Percent75.Should().BeLessOrEqualTo(100);
            stepStats.Ok.DataTransfer.MinBytes.Should().Be(2);
            stepStats.Ok.DataTransfer.AllBytes.Should().BeGreaterOrEqualTo(1000L);
        }
    }
}

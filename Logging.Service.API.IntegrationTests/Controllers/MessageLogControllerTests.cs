using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using FizzWare.NBuilder;
using Logging.Service.API.IntegrationTests.TestFramework;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace Logging.Service.API.IntegrationTests.Controllers;

public class MessageLogControllerTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _fixture;
    private readonly Faker _faker;
    private readonly Builder _builder;

    public MessageLogControllerTests(ApiWebApplicationFactory fixture)
    {
        _fixture = fixture;
        _faker = new Faker();
        _builder = new Builder();
    }

    [Fact]
    public void Upsert_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var messageLogRequests = GetMessageLogRequest();
        var dataFeed = Feed.CreateCircular("requests", messageLogRequests);
        var step = Step.Create("Upsert_Message_Logs", feed: dataFeed, async context =>
        {
            var resp = await _fixture.CreateClient()
                .PostAsJsonAsync("/MessageLogs", context.FeedItem, CancellationToken.None);

            return resp.ToNBomberResponse();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("Message_Logs", step)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(10)));

        // Act
        var stats = NBomberRunner
            .RegisterScenarios(scenario)
            .WithReportFormats(ReportFormat.Html)
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

    [Fact]
    public void Get_MessageLogs_LoadTest()
    {
        // Arrange
        var requests = GetMessageLogRequest(1000);
        var dataFeed = Feed.CreateRandom("requests", requests);

        var stepInsert = Step.Create("Upsert_Message_Logs", feed: dataFeed, async context =>
        {
            var resp = await _fixture.CreateClient()
                .PostAsJsonAsync("/MessageLogs", context.FeedItem, CancellationToken.None);

            return resp.ToNBomberResponse();
        });

        var stepGet = Step.Create("Get_Message_Logs", async _ =>
        {
            var resp = await _fixture.CreateClient()
                .GetAsync("/MessageLogs", CancellationToken.None);

            return resp.ToNBomberResponse();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("Message_Logs", stepInsert, stepGet)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(10)));

        // Act
        var stats = NBomberRunner
            .RegisterScenarios(scenario)
            .WithReportFormats(ReportFormat.Html)
            .Run();

        // Assert
        stats.Should().NotBeNull();
        var stepStats = stats.ScenarioStats[0].StepStats[0];

        stepStats.Fail.Request.Count.Should().Be(0);
        //stepStats.Ok.Request.Count.Should().BeGreaterThan(1000);
        stepStats.Ok.Request.RPS.Should().BeGreaterThan(100);
        stepStats.Ok.Latency.Percent75.Should().BeLessOrEqualTo(100);
        stepStats.Ok.DataTransfer.MinBytes.Should().Be(2);
        stepStats.Ok.DataTransfer.AllBytes.Should().BeGreaterOrEqualTo(1000L);
    }

    [Fact]
    public async Task Get_MessageLogs_IntegrationTest()
    {
        // Arrange
        var messageLogRequest = GetMessageLogRequest();

        // Act
        _ = await _fixture.CreateClient()
            .PostAsJsonAsync("/MessageLogs", messageLogRequest, CancellationToken.None);

        var result = await _fixture.CreateClient().GetAsync("/MessageLogs", CancellationToken.None);

        // Assert
        result.Should().BeSuccessful();
    }

    private IEnumerable<MessageLogRequest> GetMessageLogRequest(int requestCount = 500)
    {
        var requests = _builder.CreateListOfSize<MessageLogRequest>(requestCount).All()
            .With(x => x.MessageId = _faker.Random.AlphaNumeric(10))
            .With(x => x.MessageType = _faker.Random.AlphaNumeric(10))
            .With(x => x.MessageLogs = CreateMessageLogJson())
            .Build();

        return requests;
    }

    private JsonDocument CreateMessageLogJson()
    {
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

        var msgLogs = JsonDocument.Parse(messageLog);
        return msgLogs;
    }
}
using Bogus;
using Logging.Service.API.IntegrationTests.TestFramework;
using Logging.Service.Application.Requests;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using FluentAssertions;
using NBomber.Plugins.Http.CSharp;
using Newtonsoft.Json;
using System.Text;
using FizzWare.NBuilder;
using MessageLog.Infrastructure.Entities;

namespace Logging.Service.API.IntegrationTests.Controllers;

public class MessageLogDocControllerTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _fixture;
    private readonly Faker _faker;
    private readonly CancellationToken _cancellationToken;
    private readonly Builder _builder;

    public MessageLogDocControllerTests(ApiWebApplicationFactory fixture)
    {
        _fixture = fixture;
        _faker = new Faker();
        _builder = new Builder();
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public void Upsert_LoadTest()
    {
        var messageLogDocsData = _builder.CreateListOfSize<MessageLogDoc>(100).Build();
        var messageLogDocsFeed = Feed.CreateRandom("messageLogDocs", messageLogDocsData);

        var step = Step.Create("Upsert_Message_Logs_Doc", feed: messageLogDocsFeed, async context =>
        {
            var messageLogRequest = new MessageLogDocRequest
            {
                MessageLogDoc = context.FeedItem
            };
            using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");
            var resp = await _fixture.CreateClient()
                .PostAsync("/MessageLogDocs", httpContent, _cancellationToken);

            return resp.ToNBomberResponse();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("Message_Logs_Doc", step)
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
        stepStats.Ok.DataTransfer.MinBytes.Should().BeGreaterThanOrEqualTo(1);
        stepStats.Ok.DataTransfer.AllBytes.Should().BeGreaterOrEqualTo(1000L);
    }

    [Fact]
    public void Get_LoadTest()
    {
        var messageLogDoc = new MessageLog.Infrastructure.Entities.MessageLogDoc
        {
            MessageId = _faker.Random.AlphaNumeric(10),
            MessageType = _faker.Random.AlphaNumeric(10),
            Status = _faker.Random.AlphaNumeric(10),
            Stage = _faker.Random.AlphaNumeric(10),
            Source = _faker.Random.AlphaNumeric(10),
            Destination = _faker.Random.AlphaNumeric(10),
            Retries = _faker.Random.Number(2),
            SystemCreateDate = _faker.Date.Past(),
            SystemModifiedDate = _faker.Date.Recent(3),
            IsError = _faker.Random.Bool(),
        };

        var messageLogRequest = new MessageLogDocRequest
        {
            MessageLogDoc = messageLogDoc
        };

        var upsertStep = Step.Create("Upsert_Message_Logs_Doc", async _ =>
        {
            using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");
            var resp = await _fixture.CreateClient()
                .PostAsync("/MessageLogDocs", httpContent, _cancellationToken);

            return resp.ToNBomberResponse();
        });
        
        var getStep = Step.Create("Get_Message_Logs_Doc", async _ =>
        {
            var resp = await _fixture.CreateClient()
                .GetAsync("/MessageLogDocs", _cancellationToken);

            return resp.ToNBomberResponse();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("Message_Logs_Doc", upsertStep, getStep)
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
        stepStats.Ok.DataTransfer.MinBytes.Should().BeGreaterThanOrEqualTo(1);
        stepStats.Ok.DataTransfer.AllBytes.Should().BeGreaterOrEqualTo(1000L);
    }

    [Fact]
    public async Task Upsert_IntegrationTest()
    {
        // Arrange
        var messageLogRequest = new MessageLogDocRequest
        {
            MessageLogDoc = new MessageLog.Infrastructure.Entities.MessageLogDoc
            {
                MessageId = _faker.Random.AlphaNumeric(10),
                MessageType = _faker.Random.AlphaNumeric(10),
                Status = _faker.Random.AlphaNumeric(10),
                Stage = _faker.Random.AlphaNumeric(10),
                Source = _faker.Random.AlphaNumeric(10),
                Destination = _faker.Random.AlphaNumeric(10),
                Retries = _faker.Random.Number(2),
                SystemCreateDate = _faker.Date.Past(),
                SystemModifiedDate = _faker.Date.Recent(3),
                IsError = _faker.Random.Bool(),
            }
        };
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var result = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", httpContent, _cancellationToken);

        // Assert
        result.Should().BeSuccessful();
    }
    
    [Fact]
    public async Task Get_IntegrationTest()
    {
        // Arrange
        var messageLogRequest = new MessageLogDocRequest
        {
            MessageLogDoc = new MessageLog.Infrastructure.Entities.MessageLogDoc
            {
                MessageId = _faker.Random.AlphaNumeric(10),
                MessageType = _faker.Random.AlphaNumeric(10),
                Status = _faker.Random.AlphaNumeric(10),
                Stage = _faker.Random.AlphaNumeric(10),
                Source = _faker.Random.AlphaNumeric(10),
                Destination = _faker.Random.AlphaNumeric(10),
                Retries = _faker.Random.Number(2),
                SystemCreateDate = _faker.Date.Past(),
                SystemModifiedDate = _faker.Date.Recent(3),
                IsError = _faker.Random.Bool(),
            }
        };
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var result = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", httpContent, _cancellationToken);
        var result2 = await _fixture.CreateClient()
            .GetAsync("/MessageLogDocs", _cancellationToken);

        // Assert
        result.Should().BeSuccessful();
        result2.Should().BeSuccessful();
    }
}
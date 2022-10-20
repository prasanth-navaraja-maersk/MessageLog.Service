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
using NBomber.Contracts;
using System.Net.Http.Json;
using System.Web;

namespace Logging.Service.API.IntegrationTests.Controllers;

public class MessageLogDocControllerTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _fixture;
    private readonly Faker _faker;
    private readonly CancellationToken _cancellationToken;
    private readonly IFeed<MessageLogDoc> _messageLogDocsFeed;
    private readonly IList<MessageLogDoc> _messageLogDocsData;

    public MessageLogDocControllerTests(ApiWebApplicationFactory fixture)
    {
        _fixture = fixture;
        _faker = new Faker();
        _cancellationToken = new CancellationToken();
        var builder = new Builder();
        _messageLogDocsData = builder.CreateListOfSize<MessageLogDoc>(100).Build();
        _messageLogDocsFeed = Feed.CreateRandom("messageLogDocs", _messageLogDocsData);
    }

    [Fact]
    public void Upsert_LoadTest()
    {
        var step = Step.Create("Upsert_Message_Logs_Doc", feed: _messageLogDocsFeed, async context =>
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
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(10)))
            .WithClean(async _ =>
            {
                await _fixture.CreateClient()
                    .DeleteAsync("/MessageLogDocs");
            }); ;

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
        var upsertStep = Step.Create("Upsert_Message_Logs_Doc", feed: _messageLogDocsFeed, async context =>
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
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(10)))
            .WithClean(async _ =>
            {
                await _fixture.CreateClient()
                    .DeleteAsync("/MessageLogDocs");
            }); ;

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
    public void GetByType_LoadTest()
    {
        var upsertStep = Step.Create("Upsert_Message_Logs_Doc", feed: _messageLogDocsFeed, async context =>
        {
            var messageLogRequest = new MessageLogDocRequest
            {
                MessageLogDoc = context.FeedItem
            };
            using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

            var response = await _fixture.CreateClient()
                .PostAsync("/MessageLogDocs", httpContent, _cancellationToken);
            var nbomberResponse = response.ToNBomberResponse();

            return Response.Ok(context.FeedItem.MessageType, statusCode: nbomberResponse.StatusCode, sizeBytes: nbomberResponse.SizeBytes);
        });
        
        var getStep = Step.Create("Get_Message_Logs_Doc", async context =>
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["messageType"] = context.GetPreviousStepResponse<string>();

            var builder = new UriBuilder("http://localhost.com/MessageLogDocs/MessageType");
            builder.Port = -1;
            builder.Query = query.ToString();
            string url = builder.ToString();

            //Act
            var resp = await _fixture.CreateClient()
                .GetAsync(url, _cancellationToken);

            return resp.ToNBomberResponse();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("GetByType_Message_Logs_Doc", upsertStep, getStep)
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(10)))
            .WithClean(async _ =>
            {
                await _fixture.CreateClient()
                    .DeleteAsync("/MessageLogDocs");
            });

        // Act
        var stats = NBomberRunner
            .RegisterScenarios(scenario)
            .WithReportFormats(ReportFormat.Html)
            .Run();

        // Assert
        stats.Should().NotBeNull();
        var stepStats = stats.ScenarioStats[0].StepStats[0];

        stepStats.Ok.Request.Count.Should().BeGreaterThan(100);
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
            MessageLogDoc = _messageLogDocsData.First()
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
            MessageLogDoc = _messageLogDocsData.First()
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

    [Fact]
    public async Task Get_MessageLogs_ByMessageType_IntegrationTest()
    {
        // Arrange
        var messageLogRequest = new MessageLogDocRequest
        {
            MessageLogDoc = _messageLogDocsData.First()
        };
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Arrange
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["messageType"] = messageLogRequest.MessageLogDoc.MessageType;

        var builder = new UriBuilder("http://localhost.com/MessageLogDocs/MessageType");
        builder.Port = -1;
        builder.Query = query.ToString();
        string url = builder.ToString();

        //Act
        _ = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", httpContent, _cancellationToken);
        var result = await _fixture.CreateClient().GetAsync(url, CancellationToken.None);
        var httpResponseMessage = await _fixture.CreateClient()
            .DeleteAsync("/MessageLogDocs");
        var messageLogDocs = await result.Content.ReadFromJsonAsync<IEnumerable<MessageLogDoc>>();
        
        // Assert
        result.Should().BeSuccessful();
        httpResponseMessage.Should().BeSuccessful();
        messageLogDocs.Should().NotBeNull();
    }
}
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using Logging.Service.API.IntegrationTests.TestFramework;
using MessageLog.Infrastructure;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

namespace Logging.Service.API.IntegrationTests.Controllers;

public class ErrorLogDocumentControllerTests : IClassFixture<IntegrationTestFactory<Program, LoggingContext>>
{
    private readonly IntegrationTestFactory<Program, LoggingContext> _fixture;
    private readonly Faker _faker;

    public ErrorLogDocumentControllerTests(IntegrationTestFactory<Program, LoggingContext> fixture)
    {
        _fixture = fixture;
        _faker = new Faker();
    }

    [Fact]
    public void Upsert_ErrorLogs()
    {
        // Arrange
        var errorLogs = (new JsonObject
        {
            ["Errors"] = new JsonArray(
                new JsonObject
                {
                    ["ErrorCategory"] = "Operational", 
                    ["ErrorMessage"] = _faker.Random.AlphaNumeric(50)
                },
                new JsonObject
                {
                    ["ErrorCategory"] = "Configurational",
                    ["ErrorMessage"] = _faker.Random.AlphaNumeric(50)
                },
                new JsonObject
                {
                    ["ErrorCategory"] = "Technical",
                    ["ErrorMessage"] = _faker.Random.AlphaNumeric(50)
                })
        }).ToJsonString();

        using var errors = JsonDocument.Parse(errorLogs);
        var errorLogRequest = new ErrorLogDocumentRequest
        {
            LogMessageId = _faker.Random.AlphaNumeric(10),
            LogMessageType = _faker.Random.AlphaNumeric(10),
            ErrorLogDocuments = errors
        };

        var step = Step.Create("Upsert", async _ =>
        {
            var resp = await _fixture.CreateClient()
                .PostAsJsonAsync("/ErrorLogDocuments", errorLogRequest, CancellationToken.None);
                
            return resp.ToNBomberResponse();
        });

        var scenario = ScenarioBuilder
            .CreateScenario("Upsert_Error_Log_Documents", step)
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
}
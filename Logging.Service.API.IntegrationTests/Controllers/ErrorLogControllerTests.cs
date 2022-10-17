using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using Logging.Service.API.IntegrationTests.TestFramework;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace Logging.Service.API.IntegrationTests.Controllers
{
    public class ErrorLogControllerTests
    {
        private readonly Faker _faker;

        public ErrorLogControllerTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public async Task Upsert_ErrorLogs()
        {
            // Arrange
            var api = new ApiWebApplicationFactory();
            //var errorLogHandler = Mock.Of<IErrorLogHandler>();
            //var errorLogController = new ErrorLogController(Mock.Of<ILogger<ErrorLogController>>(), errorLogHandler);
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
            var errorLogRequest = new ErrorLogRequest
            {
                LogMessageId = _faker.Random.AlphaNumeric(10),
                LogMessageType = _faker.Random.AlphaNumeric(10),
                ErrorLogs = errors
            };

            // Act
            //var result = await messageLogController.Upsert(
            //    messageLogRequest,
            //    CancellationToken.None);
            //var result = await api.CreateClient()
            //    .PostAsync("/MessageLogs/Upsert", messageLogRequest, CancellationToken.None).Result;
            //var result = await api.CreateClient()
            //    .PostAsJsonAsync("/ErrorLogs", errorLogRequest, CancellationToken.None);

            var step = Step.Create("Upsert_Error_Logs", async context =>
            {
                var resp = await api.CreateClient()
                    .PostAsJsonAsync("/ErrorLogs", errorLogRequest, CancellationToken.None);
                return Response.Ok();
            });

            var scenario = ScenarioBuilder
                .CreateScenario("Error_Logs", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(5))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromSeconds(30)));

            var stats = NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFormats(ReportFormat.Html, ReportFormat.Md)
                .Run();

            // Assert
            stats.Should().NotBeNull();
        }
    }
}

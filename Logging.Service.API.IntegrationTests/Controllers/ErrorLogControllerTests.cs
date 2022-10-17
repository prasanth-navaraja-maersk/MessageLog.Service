using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using Logging.Service.API.IntegrationTests.TestFramework;

namespace Logging.Service.API.IntegrationTests.Controllers
{
    public class ErrorLogControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _fixture;
        private readonly Faker _faker;

        public ErrorLogControllerTests(ApiWebApplicationFactory fixture)
        {
            _fixture = fixture;
            _faker = new Faker();
        }

        [Fact]
        public async Task Upsert_ErrorLogs()
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
            var errorLogRequest = new ErrorLogRequest
            {
                LogMessageId = _faker.Random.AlphaNumeric(10),
                LogMessageType = _faker.Random.AlphaNumeric(10),
                ErrorLogs = errors
            };

            // Act
            var result = await _fixture.CreateClient()
                .PostAsJsonAsync("/ErrorLogs", errorLogRequest, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }
    }
}

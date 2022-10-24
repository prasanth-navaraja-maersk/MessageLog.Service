using System.Text;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using FizzWare.NBuilder;
using Logging.Service.API.IntegrationTests.TestFramework;
using MessageLog.Infrastructure;
using Newtonsoft.Json;

namespace Logging.Service.API.IntegrationTests.Controllers;

public class IntegratedServiceScenarios : IClassFixture<IntegrationTestFactory<Program, LoggingContext>>
{
    private readonly IntegrationTestFactory<Program, LoggingContext> _fixture;
    private readonly Faker _faker;
    private readonly Builder _builder;

    public IntegratedServiceScenarios(IntegrationTestFactory<Program, LoggingContext> fixture)
    {
        _fixture = fixture;
        _builder = new Builder();
        _faker = new Faker();
    }

    [Fact]
    public async Task TO_MessageLogs()
    {
        // Arrange
        var correlationId = _faker.Random.Long(16);
        var transportOrderNumber = _faker.Random.AlphaNumeric(10);

        //TO Inbound
        var messageLogRequest = _builder.CreateNew<MessageLogRequest>()
            .With(m => m.MessageLog = _builder.CreateNew<MessageLog.Infrastructure.Entities.MessageLog>()
                .With(x => x.ExternalIdentifier = transportOrderNumber)
                .With(x => x.CorrelationId = correlationId)
                .With(x => x.Source = "NeoNav")
                .With(x => x.Destination = "FA&P")
                .With(x => x.MessageType = "TransportOrderInbound")
                .Build())
            .Build();
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var result = await _fixture.CreateClient()
            .PostAsync("/MessageLogs", httpContent, CancellationToken.None);

        //TO Outbound
        var outboundMessageLogRequest = _builder.CreateNew<MessageLogRequest>()
            .With(m => m.MessageLog = _builder.CreateNew<MessageLog.Infrastructure.Entities.MessageLog>()
                .With(x => x.ExternalIdentifier = transportOrderNumber)
                .With(x => x.CorrelationId = correlationId)
                .With(x => x.Source = "FA&P")
                .With(x => x.Destination = "Envista")
                .With(x => x.MessageType = "TransportOrderOutbound")
                .Build())
            .Build();
        using var outboundHttpContent = new StringContent(JsonConvert.SerializeObject(outboundMessageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var outboundResult = await _fixture.CreateClient()
            .PostAsync("/MessageLogs", outboundHttpContent, CancellationToken.None);

        // Assert
        result.Should().BeSuccessful();
        outboundResult.Should().BeSuccessful();
    }

    [Fact]
    public async Task SI_MessageLogs()
    {
        // Arrange
        var correlationId = _faker.Random.Long(16);
        var externalInvoiceId = _faker.Random.AlphaNumeric(10);

        //Si Inbound
        var messageLogRequest = _builder.CreateNew<MessageLogRequest>()
            .With(m => m.MessageLog = _builder.CreateNew<MessageLog.Infrastructure.Entities.MessageLog>()
                .With(x => x.ExternalIdentifier = externalInvoiceId)
                .With(x => x.CorrelationId = correlationId)
                .With(x => x.Source = "Envista")
                .With(x => x.Destination = "FA&P")
                .With(x => x.MessageType = "SupplierInvoiceInbound")
                .Build())
            .Build();
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var result = await _fixture.CreateClient()
            .PostAsync("/MessageLogs", httpContent, CancellationToken.None);

        //SI Outbound
        var outboundMessageLogRequest = _builder.CreateNew<MessageLogRequest>()
            .With(m => m.MessageLog = _builder.CreateNew<MessageLog.Infrastructure.Entities.MessageLog>()
                .With(x => x.ExternalIdentifier = externalInvoiceId)
                .With(x => x.CorrelationId = correlationId)
                .With(x => x.Source = "FA&P")
                .With(x => x.Destination = "Seeburger")
                .With(x => x.MessageType = "SupplierInvoiceOutbound")
                .Build())
            .Build();
        using var outboundHttpContent = new StringContent(JsonConvert.SerializeObject(outboundMessageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var outboundResult = await _fixture.CreateClient()
            .PostAsync("/MessageLogs", outboundHttpContent, CancellationToken.None);

        // Assert
        result.Should().BeSuccessful();
        outboundResult.Should().BeSuccessful();
    }
}
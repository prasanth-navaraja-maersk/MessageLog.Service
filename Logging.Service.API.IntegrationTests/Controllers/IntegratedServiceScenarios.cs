using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FluentAssertions;
using Logging.Service.Application.Requests;
using Bogus;
using FizzWare.NBuilder;
using Logging.Service.API.IntegrationTests.TestFramework;
using MessageLog.Infrastructure.Entities;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using Newtonsoft.Json;

namespace Logging.Service.API.IntegrationTests.Controllers;

public class IntegratedServiceScenarios : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _fixture;
    private readonly Faker _faker;
    private readonly Builder _builder;

    public IntegratedServiceScenarios(ApiWebApplicationFactory fixture)
    {
        _fixture = fixture;
        _builder = new Builder();
        _faker = new Faker();
    }

    [Fact]
    public async Task TO_MessageLogs()
    {
        // Arrange
        var messageId = _faker.Random.AlphaNumeric(16);
        var transportOrderNumber = _faker.Random.AlphaNumeric(10);

        //TO Inbound
        var messageLogRequest = _builder.CreateNew<MessageLogDocRequest>()
            .With(m => m.MessageLogDoc = _builder.CreateNew<MessageLogDoc>()
                .With(x => x.ExternalIdentifier = transportOrderNumber)
                .With(x => x.MessageId = messageId)
                .With(x => x.Source = "NeoNav")
                .With(x => x.Destination = "FA&P")
                .With(x => x.MessageType = "TransportOrderInbound")
                .Build())
            .Build();
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var result = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", httpContent, CancellationToken.None);

        //TO Outbound
        var outboundMessageLogRequest = _builder.CreateNew<MessageLogDocRequest>()
            .With(m => m.MessageLogDoc = _builder.CreateNew<MessageLogDoc>()
                .With(x => x.ExternalIdentifier = transportOrderNumber)
                .With(x => x.MessageId = messageId)
                .With(x => x.Source = "FA&P")
                .With(x => x.Destination = "Envista")
                .With(x => x.MessageType = "TransportOrderOutbound")
                .Build())
            .Build();
        using var outboundHttpContent = new StringContent(JsonConvert.SerializeObject(outboundMessageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var outboundResult = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", outboundHttpContent, CancellationToken.None);

        // Assert
        result.Should().BeSuccessful();
        outboundResult.Should().BeSuccessful();
    }

    [Fact]
    public async Task SI_MessageLogs()
    {
        // Arrange
        var messageId = _faker.Random.AlphaNumeric(16);
        var externalInvoiceId = _faker.Random.AlphaNumeric(10);

        //Si Inbound
        var messageLogRequest = _builder.CreateNew<MessageLogDocRequest>()
            .With(m => m.MessageLogDoc = _builder.CreateNew<MessageLogDoc>()
                .With(x => x.ExternalIdentifier = externalInvoiceId)
                .With(x => x.MessageId = messageId)
                .With(x => x.Source = "Envista")
                .With(x => x.Destination = "FA&P")
                .With(x => x.MessageType = "SupplierInvoiceInbound")
                .Build())
            .Build();
        using var httpContent = new StringContent(JsonConvert.SerializeObject(messageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var result = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", httpContent, CancellationToken.None);

        //SI Outbound
        var outboundMessageLogRequest = _builder.CreateNew<MessageLogDocRequest>()
            .With(m => m.MessageLogDoc = _builder.CreateNew<MessageLogDoc>()
                .With(x => x.ExternalIdentifier = externalInvoiceId)
                .With(x => x.MessageId = messageId)
                .With(x => x.Source = "FA&P")
                .With(x => x.Destination = "Seeburger")
                .With(x => x.MessageType = "SupplierInvoiceOutbound")
                .Build())
            .Build();
        using var outboundHttpContent = new StringContent(JsonConvert.SerializeObject(outboundMessageLogRequest), Encoding.UTF8, "application/json");

        // Act
        var outboundResult = await _fixture.CreateClient()
            .PostAsync("/MessageLogDocs", outboundHttpContent, CancellationToken.None);

        // Assert
        result.Should().BeSuccessful();
        outboundResult.Should().BeSuccessful();
    }
}
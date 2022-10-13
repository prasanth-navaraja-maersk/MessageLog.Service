namespace MessageLog.Service.Tests;

public class LoggingServiceTests
{
    [Fact]
    public void InsertMessageLogs_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var service = new LoggingService(null);
        Entities.MessageLog messageLog = null;

        // Act
        var result = service.InsertMessageLogs(
            messageLog);

        // Assert
        Assert.True(false);
    }
}
using MessageLog.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Service.API.IntegrationTests.TestFramework;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove AppDbContext
            services.RemoveDbContext<LoggingContext>();

            // Add DB context pointing to test container
            services.AddDbContext<LoggingContext>(options => { options.UseNpgsql("the new connection string"); });

            // Ensure schema gets created
            services.EnsureDbCreated<LoggingContext>();
        });
    }
}
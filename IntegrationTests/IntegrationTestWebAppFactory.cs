using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using Testcontainers.MsSql;

namespace IntegrationTests;

public class IntegrationTestWebAppFactory
    : WebApplicationFactory<Program>,
        IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("Strong_password_123!")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<ApplicationDbContext>);

            var descriptor = Enumerable
                .SingleOrDefault<ServiceDescriptor>(services, s => s.ServiceType == descriptorType);

            if (descriptor is not null) services.Remove(descriptor);

            EntityFrameworkServiceCollectionExtensions.AddDbContext<ApplicationDbContext>(services, options =>
                SqlServerDbContextOptionsExtensions.UseSqlServer(options, _dbContainer.GetConnectionString()));
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
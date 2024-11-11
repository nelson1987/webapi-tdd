using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>,
        IDisposable
{
    private readonly IServiceScope _scope;

    //protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    protected readonly HttpClient _client;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        //Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        _client = factory.CreateDefaultClient();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}
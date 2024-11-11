using FluentValidation;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Dependencies
{
    public static void AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<CreateUser.IHandler, CreateUser.Handler>()
            .AddScoped<IValidator<CreateUser.Command>, CreateUser.CommandValidator>()
            .AddInfrastructure();
    }
}
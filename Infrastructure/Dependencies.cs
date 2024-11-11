using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(string.Empty))
            .AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}
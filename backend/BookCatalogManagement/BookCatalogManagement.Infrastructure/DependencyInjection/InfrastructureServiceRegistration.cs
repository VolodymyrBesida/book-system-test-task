using BookCatalogManagement.Domain.Interfaces.Repositories;
using BookCatalogManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookCatalogManagement.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBookRepository, BookRepository>();
        return services;
    }
}
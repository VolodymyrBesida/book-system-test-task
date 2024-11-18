using BookCatalogManagement.Application.Interfaces.Services;
using BookCatalogManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookCatalogManagement.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}

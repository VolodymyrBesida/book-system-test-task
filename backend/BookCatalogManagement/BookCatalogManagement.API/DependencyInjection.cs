using Microsoft.Extensions.DependencyInjection;

namespace BookCatalogManagement.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Book Catalog API",
                Version = "v1",
                Description = "API for managing a book catalog"
            });
        });
        services.AddControllers();

        return services;
    }
}
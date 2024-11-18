using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using BookCatalogManagement.API;
using BookCatalogManagement.Application.DependencyInjection;
using BookCatalogManagement.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading.RateLimiting;
using System;
using BookCatalogManagement.API.Endpoints;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()  // Allows all origins
              .AllowAnyHeader()  // Allows any headers
              .AllowAnyMethod(); // Allows any HTTP method (GET, POST, etc.)
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Catalog API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAnyOrigin");

// Map endpoints
app.MapBooksEndpoints();

app.Run();

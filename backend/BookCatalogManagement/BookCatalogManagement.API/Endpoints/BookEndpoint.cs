using BookCatalogManagement.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using System;
using BookCatalogManagement.Application.Interfaces.Services;

namespace BookCatalogManagement.API.Endpoints;

public static class BooksEndpoints
{
    public static IEndpointRouteBuilder MapBooksEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/books");

        group.MapGet("/", GetBooks);
        group.MapGet("/{id:guid}", GetBookById);
        group.MapPost("/", CreateBook);
        group.MapPut("/{id:guid}", UpdateBook);
        group.MapDelete("/{id:guid}", DeleteBook);
        group.MapPost("/upload", UploadBooks).DisableAntiforgery(); ;

        return app;
    }

    private static async Task<IResult> GetBooks(IBookService _service, string? title, string? author, string? genre, int page = 1, int pageSize = 10, string? sortField = "title", bool ascending = true)
    {
        var books = await _service.GetBooksAsync(title, author, genre, page, pageSize, sortField, ascending);
        return Results.Ok(books);
    }

    private static async Task<IResult> GetBookById(IBookService _service, Guid id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return book is not null ? Results.Ok(book) : Results.NotFound();
    }

    private static async Task<IResult> CreateBook(IBookService _service, Book book)
    {
        await _service.AddBookAsync(book);
        return Results.Created($"/books/{book.Id}", book);
    }

    private static async Task<IResult> UpdateBook(IBookService _service, Guid id, Book updatedBook)
    {
        await _service.UpdateBookAsync(id, updatedBook);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteBook(IBookService _service, Guid id)
    {
        await _service.DeleteBookAsync(id);
        return Results.NoContent();
    }

    private static async Task<IResult> UploadBooks(
        IBookService _service,
        IFormFile file)
    {

        if (file.ContentType is not "text/csv")
            return Results.BadRequest("Invalid file format");
        await _service.BulkUploadBooksAsync(file);
        return Results.Ok();
    }
}
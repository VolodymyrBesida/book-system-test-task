using BookCatalogManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BookCatalogManagement.Application.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetBooksAsync(string? title, string? author, string? genre, int page, int pageSize, string? sortField, bool ascending, CancellationToken cancellationToken = default);
    Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddBookAsync(Book book, CancellationToken cancellationToken = default);
    Task UpdateBookAsync(Guid id, Book updatedBook, CancellationToken cancellationToken = default);
    Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    Task BulkUploadBooksAsync(IFormFile file, CancellationToken cancellationToken = default);
}

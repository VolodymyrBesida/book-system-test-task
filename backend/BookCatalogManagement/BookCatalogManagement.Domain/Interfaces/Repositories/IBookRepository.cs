using BookCatalogManagement.Domain.Entities;

namespace BookCatalogManagement.Domain.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(string? title, string? author, string? genre, int page, int pageSize, string? sortField, bool ascending, CancellationToken cancellationToken = default);
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Book book, CancellationToken cancellationToken = default);
    Task UpdateAsync(Book book, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task BulkUploadAsync(IEnumerable<Book> books, CancellationToken cancellationToken = default);
}
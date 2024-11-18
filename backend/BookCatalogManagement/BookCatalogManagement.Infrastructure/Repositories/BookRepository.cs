using BookCatalogManagement.Domain.Entities;
using BookCatalogManagement.Domain.Interfaces.Repositories;
using BookCatalogManagement.Infrastructure.Extensions;

namespace BookCatalogManagement.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private readonly object _lock = new object();

    public async Task<IEnumerable<Book>> GetAllAsync(
        string? title,
        string? author,
        string? genre,
        int page,
        int pageSize,
        string? sortField,
        bool ascending,
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            IQueryable<Book> query = _books.AsQueryable()
           .FilterByTitle(title)
           .FilterByAuthor(author)
           .FilterByGenre(genre);

            query = sortField switch
            {
                "title" => ascending ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title),
                "author" => ascending ? query.OrderBy(b => b.Author) : query.OrderByDescending(b => b.Author),
                "genre" => ascending ? query.OrderBy(b => b.Genre) : query.OrderByDescending(b => b.Genre),
                _ => query
            };

            var paginatedResult = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult((IEnumerable<Book>)paginatedResult);
        }, cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }, cancellationToken);
    }
    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            _books.Add(book);
        }, cancellationToken);
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            lock (_lock)
            {
                var existingBook = _books
                    .FirstOrDefault(b => b.Id == book.Id) ??
                        throw new ArgumentNullException(nameof(book), "The book with the specified ID was not found.");

                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
            }
        }, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            var book = _books.FirstOrDefault(b => b.Id == id) ??
                throw new ArgumentNullException("The book with the specified ID was not found.");
            _books.Remove(book);
        }, cancellationToken);
    }

    public async Task BulkUploadAsync(IEnumerable<Book> books, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            _books.AddRange(books);
        }, cancellationToken);
    }
}
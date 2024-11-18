using BookCatalogManagement.Application.Interfaces.Services;
using BookCatalogManagement.Domain.Entities;
using BookCatalogManagement.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace BookCatalogManagement.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync(string? title, string? author, string? genre, int page, int pageSize, string? sortField, bool ascending, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(title, author, genre, page, pageSize, sortField, ascending,cancellationToken);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default) => await _repository.GetByIdAsync(id, cancellationToken);

    public async Task AddBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        book.Id = Guid.NewGuid();
        await _repository.AddAsync(book);
    }

    public async Task UpdateBookAsync(Guid id, Book updatedBook, CancellationToken cancellationToken = default)
    {
        var existingBook = await _repository.GetByIdAsync(id);
        if (existingBook is null)
            throw new KeyNotFoundException("Book not found");

        updatedBook.Id = id;
        await _repository.UpdateAsync(updatedBook, cancellationToken);
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default) => await _repository.DeleteAsync(id, cancellationToken);

    public async Task BulkUploadBooksAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var books = new List<Book>();

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (line is null) continue;

            var parts = line.Split(',');
            if (parts.Length != 3) continue;

            books.Add(new Book
            {
                Id = Guid.NewGuid(),
                Title = parts[0],
                Author = parts[1],
                Genre = parts[2]
                //Year = int.Parse(parts[3])
            });
        }
        await _repository.BulkUploadAsync(books, cancellationToken);
    }
}

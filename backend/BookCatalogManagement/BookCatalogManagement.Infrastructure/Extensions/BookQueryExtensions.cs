using BookCatalogManagement.Domain.Entities;

namespace BookCatalogManagement.Infrastructure.Extensions;

public static class BookQueryExtensions
{
    public static IQueryable<Book> FilterByTitle(this IQueryable<Book> query, string? title) =>
        string.IsNullOrWhiteSpace(title)
            ? query
            : query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

    public static IQueryable<Book> FilterByAuthor(this IQueryable<Book> query, string? author) =>
        string.IsNullOrWhiteSpace(author)
            ? query
            : query.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

    public static IQueryable<Book> FilterByGenre(this IQueryable<Book> query, string? genre) =>
        string.IsNullOrWhiteSpace(genre)
            ? query
            : query.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
}

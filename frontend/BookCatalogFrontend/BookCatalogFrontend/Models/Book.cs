using System.ComponentModel.DataAnnotations;

namespace BookCatalogFrontend.Models;

public class Book
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Author is required.")]
    public string Author { get; set; } = string.Empty;
    [Required(ErrorMessage = "Genre is required.")]
    public string Genre { get; set; } = string.Empty;
}
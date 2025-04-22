using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos;

public record class CreateGame(
    [Required][StringLength(50)]string Name,
    [Required][StringLength(10)]string Genre,
    [Range(1,100)]decimal Price,
    DateOnly ReleaseDate);

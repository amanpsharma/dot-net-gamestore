using System.ComponentModel.DataAnnotations;

namespace Games.Dtos;

public record class CreateGamesDtos(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseData
);
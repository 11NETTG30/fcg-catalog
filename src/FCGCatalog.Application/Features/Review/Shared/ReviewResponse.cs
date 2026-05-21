namespace FCGCatalog.Application.Features.Review.Shared;

public record ReviewResponse(
    Guid Id,
    Guid UsuarioId,
    Guid JogoId,
    int Nota,
    string? Comentario,
    DateTime DataCriacao
);
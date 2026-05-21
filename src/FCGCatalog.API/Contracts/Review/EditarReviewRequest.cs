namespace FCGCatalog.API.Contracts.Review;

public sealed record EditarReviewRequest(
    Guid Id,
    int Nota,
    string? Comentario
);

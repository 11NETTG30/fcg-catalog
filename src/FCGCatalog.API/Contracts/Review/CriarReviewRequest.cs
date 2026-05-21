namespace FCGCatalog.API.Contracts.ReviewJogo;

public sealed record CriarReviewRequest(
    Guid JogoId, 
    int Nota,
    string Comentario
);

using FCGCatalog.Application.Features.Review.Shared;
using FCGCatalog.Domain.Repositories;
using MediatR;

namespace FCGCatalog.Application.Features.Review.ObterReviewsPorJogo;

public sealed class ObterReviewsPorJogoHandler
        : IRequestHandler<ObterReviewsPorJogoQuery, IEnumerable<ReviewResponse>>
{
    private readonly IReviewRepository _repository;

    public ObterReviewsPorJogoHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReviewResponse>> Handle(
        ObterReviewsPorJogoQuery query,
        CancellationToken cancellationToken)
    {
        var reviews = await _repository.ObterReviewsPorJogo(query.Id, cancellationToken);

        var response = reviews.Select(x => new ReviewResponse(
            Id: x.Id,
            UsuarioId: x.UsuarioId,
            JogoId: x.JogoId,
            Nota: x.Nota,
            Comentario: x.Comentario,
            DataCriacao: x.DataCriacao
        )).ToList();

        return response;
    }
}
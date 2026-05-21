using FCGCatalog.Application.Features.Review.Shared;
using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Review.ObterReviewPorId;

public sealed class ObterReviewPorIdHandler
        : IRequestHandler<ObterReviewPorIdQuery, ReviewResponse>
{
    private readonly IReviewRepository _repository;

    public ObterReviewPorIdHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReviewResponse> Handle(
        ObterReviewPorIdQuery query,
        CancellationToken cancellationToken)
    {
        var review = await _repository.ObterPorId(query.Id, cancellationToken);

        if (review is null)
            throw new NotFoundException($"A review de id {query.Id} não foi encontrada.");

        var response = new ReviewResponse(
            Id: review.Id,
            UsuarioId: review.UsuarioId,
            JogoId: review.JogoId,
            Nota: review.Nota,
            Comentario: review.Comentario,
            DataCriacao: review.DataCriacao
        );

        return response;
    }
}
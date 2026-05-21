using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Review.DeletarReview;

public sealed class DeletarReviewHandler : IRequestHandler<DeletarReviewCommand, Unit>
{
    private readonly IReviewRepository _repository;

    public DeletarReviewHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeletarReviewCommand command,
        CancellationToken cancellationToken)
    {
        var review = await _repository.ObterPorId(command.Id, cancellationToken);

        if (review is null)
            throw new NotFoundException($"A review de id {command.Id} não foi encontrada.");

        if (review.UsuarioId != command.UsuarioId)
            throw new UnauthorizedAccessException("O usuário não tem permissão para deletar essa review.");

        await _repository.Deletar(review, cancellationToken);

        return Unit.Value;
    }
}

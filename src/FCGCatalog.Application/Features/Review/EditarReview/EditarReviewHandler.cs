using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;

namespace FCGCatalog.Application.Features.Review.EditarReview;

public sealed class EditarReviewHandler : IRequestHandler<EditarReviewCommand, Unit>
{
    private readonly IReviewRepository _repository;

    public EditarReviewHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(EditarReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _repository.ObterPorId(request.Id, cancellationToken);

        if (review is null)
            throw new NotFoundException($"A review de id {request.Id} não foi encontrada.");

        if (review.UsuarioId != request.UsuarioId)
            throw new UnauthorizedAccessException("O usuário não tem permissão para editar esta review.");

        review.Editar(nota: request.Nota,
            comentario: request.Comentario);

        await _repository.Atualizar(review, cancellationToken);

        return Unit.Value;
    }
}
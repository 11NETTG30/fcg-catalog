using FCGCatalog.Domain.Repositories;
using FCGCatalog.Domain.Shared.Exceptions;
using MediatR;
using ReviewDomain = FCGCatalog.Domain.Entities.Review;

namespace FCGCatalog.Application.Features.Review.CriarReview;

public sealed class CriarReviewHandler : IRequestHandler<CriarReviewCommand, CriarReviewResponse>
{
    private readonly IReviewRepository _repository;

    public CriarReviewHandler(IReviewRepository repository) 
    {
        _repository = repository;
    }

    public async Task<CriarReviewResponse> Handle(
        CriarReviewCommand command,
        CancellationToken cancellationToken)
    {
        var reviewJaExiste = await _repository.ExistePorUsuarioEJogo(command.UsuarioId, command.JogoId, cancellationToken);

        if (reviewJaExiste)
            throw new ConflictException("O usuário já fez um review deste jogo.");

        var review = ReviewDomain.Criar(
            usuarioId: command.UsuarioId,
            jogoId: command.JogoId,
            nota: command.Nota,
            comentario: command.Comentario);

        await _repository.Adicionar(review, cancellationToken);

        return new CriarReviewResponse(review.Id);
    }
}

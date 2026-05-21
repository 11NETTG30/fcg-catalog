using FCGCatalog.Domain.Shared.Abstractions;
using FCGCatalog.Domain.Shared.Exceptions;

namespace FCGCatalog.Domain.Entities;

public sealed class Review : Entity, IAggregateRoot
{
    public Guid UsuarioId { get; private set; }

    public Guid JogoId { get; private set; }

    public int Nota { get; private set; }
    public string Comentario { get; private set; } = string.Empty;

    public DateTime DataCriacao { get; private set; }

    private Review(Guid usuarioId, Guid jogoId, int nota, string? comentario)
    {
        SetUsuario(usuarioId);
        SetJogo(jogoId);
        SetNota(nota);
        SetComentario(comentario);
        DataCriacao = DateTime.UtcNow;
    }

    private Review() { }

    public static Review Criar(Guid usuarioId, Guid jogoId, int nota, string? comentario)
        => new Review(usuarioId, jogoId, nota, comentario);

    public void SetUsuario(Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
            throw new ValidationException("ID do usuário é obrigatório");

        UsuarioId = usuarioId;
    }

    public void SetJogo(Guid jogoId)
    {
        if (jogoId == Guid.Empty)
            throw new ValidationException("ID do jogo é obrigatório");

        JogoId = jogoId;
    }

    public void SetNota(int nota)
    {
        if (nota < 1 || nota > 5)
            throw new ValidationException("Nota deve ser entre 1 e 5");

        Nota = nota;
    }

    public void SetComentario(string? comentario)
    {
        if (comentario?.Length > 1000)
            throw new ValidationException("Comentário deve ter no máximo 1000 caracteres");

        Comentario = comentario?.Trim() ?? string.Empty;
    }

    public void Editar(int nota, string? comentario)
    {
        SetNota(nota);
        SetComentario(comentario);
    }
}

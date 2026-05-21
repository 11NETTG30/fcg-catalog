using FCGCatalog.Domain.Entities;
using FCGCatalog.Domain.Shared.Exceptions;

namespace FCG.Catalog.Tests.Domain.Entities;

public class ReviewTests
{
    [Fact]
    public void Criar_DeveRetornarReviewValida_QuandoDadosValidos()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var nota = 4;
        var comentario = "Excelente jogo!";

        var review = Review.Criar(usuarioId, jogoId, nota, comentario);

        Assert.NotNull(review);
        Assert.Equal(usuarioId, review.UsuarioId);
        Assert.Equal(jogoId, review.JogoId);
        Assert.Equal(nota, review.Nota);
        Assert.Equal(comentario, review.Comentario);
        Assert.True(review.DataCriacao <= DateTime.UtcNow);
        Assert.True(review.DataCriacao > DateTime.UtcNow.AddSeconds(-1));
    }

    [Fact]
    public void Criar_DeveRetornarReviewSemComentario_QuandoComentarioNulo()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var nota = 3;

        var review = Review.Criar(usuarioId, jogoId, nota, null);

        Assert.NotNull(review);
        Assert.Equal(string.Empty, review.Comentario);
    }

    [Fact]
    public void Criar_DeveLancarValidationException_QuandoUsuarioIdVazio()
    {
        var jogoId = Guid.NewGuid();
        var nota = 4;
        var comentario = "Bom jogo";

        var exception = Assert.Throws<ValidationException>(() =>
            Review.Criar(Guid.Empty, jogoId, nota, comentario));

        Assert.Equal("ID do usuário é obrigatório", exception.Message);
    }

    [Fact]
    public void Criar_DeveLancarValidationException_QuandoJogoIdVazio()
    {
        var usuarioId = Guid.NewGuid();
        var nota = 4;
        var comentario = "Bom jogo";

        var exception = Assert.Throws<ValidationException>(() =>
            Review.Criar(usuarioId, Guid.Empty, nota, comentario));

        Assert.Equal("ID do jogo é obrigatório", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(10)]
    public void Criar_DeveLancarValidationException_QuandoNotaInvalida(int notaInvalida)
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var comentario = "Comentário";

        var exception = Assert.Throws<ValidationException>(() =>
            Review.Criar(usuarioId, jogoId, notaInvalida, comentario));

        Assert.Equal("Nota deve ser entre 1 e 5", exception.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Criar_DeveAceitarNotasValidas(int notaValida)
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var comentario = "Comentário";

        var review = Review.Criar(usuarioId, jogoId, notaValida, comentario);

        Assert.Equal(notaValida, review.Nota);
    }

    [Fact]
    public void Criar_DeveLancarValidationException_QuandoComentarioMuitoLongo()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var nota = 4;
        var comentarioLongo = new string('a', 1001);

        var exception = Assert.Throws<ValidationException>(() =>
            Review.Criar(usuarioId, jogoId, nota, comentarioLongo));

        Assert.Equal("Comentário deve ter no máximo 1000 caracteres", exception.Message);
    }

    [Fact]
    public void Criar_DeveAceitarComentarioCom1000Caracteres()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var nota = 4;
        var comentario1000 = new string('a', 1000);

        var review = Review.Criar(usuarioId, jogoId, nota, comentario1000);

        Assert.Equal(1000, review.Comentario.Length);
    }

    [Fact]
    public void Criar_DeveRemoverEspacosEmBrancoDoComentario()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var nota = 4;
        var comentario = "  Comentário com espaços  ";

        var review = Review.Criar(usuarioId, jogoId, nota, comentario);

        Assert.Equal("Comentário com espaços", review.Comentario);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void Criar_DeveManterComentarioVazio_QuandoComentarioEmBranco(string comentarioEmBranco)
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var nota = 4;

        var review = Review.Criar(usuarioId, jogoId, nota, comentarioEmBranco);

        Assert.Equal(string.Empty, review.Comentario);
    }

    [Fact]
    public void SetUsuario_DeveAtualizarUsuarioId_QuandoIdValido()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");
        var novoUsuarioId = Guid.NewGuid();

        review.SetUsuario(novoUsuarioId);

        Assert.Equal(novoUsuarioId, review.UsuarioId);
    }

    [Fact]
    public void SetUsuario_DeveLancarValidationException_QuandoIdVazio()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");

        var exception = Assert.Throws<ValidationException>(() =>
            review.SetUsuario(Guid.Empty));

        Assert.Equal("ID do usuário é obrigatório", exception.Message);
    }

    [Fact]
    public void SetJogo_DeveAtualizarJogoId_QuandoIdValido()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");
        var novoJogoId = Guid.NewGuid();

        review.SetJogo(novoJogoId);

        Assert.Equal(novoJogoId, review.JogoId);
    }

    [Fact]
    public void SetJogo_DeveLancarValidationException_QuandoIdVazio()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");

        var exception = Assert.Throws<ValidationException>(() =>
            review.SetJogo(Guid.Empty));

        Assert.Equal("ID do jogo é obrigatório", exception.Message);
    }

    [Fact]
    public void SetNota_DeveAtualizarNota_QuandoNotaValida()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");

        review.SetNota(5);

        Assert.Equal(5, review.Nota);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void SetNota_DeveLancarValidationException_QuandoNotaInvalida(int notaInvalida)
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");

        var exception = Assert.Throws<ValidationException>(() =>
            review.SetNota(notaInvalida));

        Assert.Equal("Nota deve ser entre 1 e 5", exception.Message);
    }

    [Fact]
    public void SetComentario_DeveAtualizarComentario_QuandoComentarioValido()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");
        var novoComentario = "Novo comentário atualizado";

        review.SetComentario(novoComentario);

        Assert.Equal(novoComentario, review.Comentario);
    }

    [Fact]
    public void SetComentario_DeveLancarValidationException_QuandoComentarioMuitoLongo()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");
        var comentarioLongo = new string('x', 1001);

        var exception = Assert.Throws<ValidationException>(() =>
            review.SetComentario(comentarioLongo));

        Assert.Equal("Comentário deve ter no máximo 1000 caracteres", exception.Message);
    }

    [Fact]
    public void Editar_DeveAtualizarNotaEComentario_QuandoDadosValidos()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Comentário inicial");
        var novaNota = 5;
        var novoComentario = "Comentário atualizado";

        review.Editar(novaNota, novoComentario);

        Assert.Equal(novaNota, review.Nota);
        Assert.Equal(novoComentario, review.Comentario);
    }

    [Fact]
    public void Editar_DeveLancarValidationException_QuandoNotaInvalida()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");

        var exception = Assert.Throws<ValidationException>(() =>
            review.Editar(10, "Novo comentário"));

        Assert.Equal("Nota deve ser entre 1 e 5", exception.Message);
    }

    [Fact]
    public void Editar_DeveLancarValidationException_QuandoComentarioMuitoLongo()
    {
        var review = Review.Criar(Guid.NewGuid(), Guid.NewGuid(), 3, "Teste");
        var comentarioLongo = new string('y', 1001);

        var exception = Assert.Throws<ValidationException>(() =>
            review.Editar(4, comentarioLongo));

        Assert.Equal("Comentário deve ter no máximo 1000 caracteres", exception.Message);
    }
}
